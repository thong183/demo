
using Acme.News_Website.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Web;
using Grpc.Core;
using Volo.Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class ImageRepository : EfCoreRepository<News_WebsiteDbContext, Image, Guid>, IImageRepository
    {
        private readonly IHostEnvironment _env;
        private readonly IRepository<Image, Guid> _image;
        private readonly IConfiguration _config;
        public ImageRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider, IHostEnvironment env, IRepository<Image, Guid> image, IConfiguration config) : base(dbContextProvider)
        {
            _env = env;
            _image = image;
            _config = config;
        }
        //public string MapPath(string path);
        [Obsolete]
        public List<string> UploadImages(List<IFormFile> images, Guid idUser)
        {
            var ListImage = new List<string>();
            foreach (var image in images)
            {
                var ext = new List<string>()
                {
                    "png", "jpeg", "jpg" ,"PNG","JPG","JPEG","gif","GIF"
                };
                if (ext.Contains(Path.GetExtension(image.FileName)))
                {
                    var uniqueFileName = GetUniqueFileName(image.FileName);
                    var dir = Path.Combine(_env.ContentRootPath, "wwwroot/Images");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var filePath = Path.Combine(dir, uniqueFileName);
                    //var filePath = Server.MapPath("/Images")
                    var fileUrl = _config["App:SelfUrl"] + "/Images/" + uniqueFileName;
                    image.CopyTo(new FileStream(filePath, FileMode.Create));
                    SaveImagePathToDb(fileUrl, idUser);
                    ListImage.Add(fileUrl);
                }
                else
                {
                    throw new Exception("Please choose correct image file !");
                }
            }
            return ListImage;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        public async Task RemoveImageFromDir(string fileName)
        {
            var dir = Path.Combine(_env.ContentRootPath, "wwwroot/Images");
            // Check if file exists with its full path    
            if (File.Exists(Path.Combine(dir, fileName)))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(dir, fileName));
            }
            else throw new Exception("File not found");
        }
    [Obsolete]
    private void SaveImagePathToDb(string filepath, Guid idUser)
    {
        bool title = false;
        var image = new Image()
        {
            IdUser = idUser,
            ImageUrl = filepath,
            UserProfileImg = title

        };
        _image.InsertAsync(image);
    }
    [Obsolete]
    public bool SetUserProfileImage(Guid idUser, Guid IdImage)
    {
        var image = DbContext.Images.Find(IdImage);
        if (GetUserProfileImage(idUser) == null)
        {
            image.UserProfileImg = true;
            return true;
        }
        else
        {
            var oldImg = GetUserProfileImage(idUser);
            oldImg.UserProfileImg = false;
            image.UserProfileImg = true;
            return true;
        }
    }

    [Obsolete]
    public string GetImageUrl(Guid Img)
    {
        return DbContext.Images.Find(Img).ImageUrl;
    }
    [Obsolete]
    public Image GetUserProfileImage(Guid idUser)
    {
        if (DbContext.Images.Any(x => x.IdUser == idUser && x.UserProfileImg == true))
        {
            return DbContext.Images.SingleOrDefault(x => x.IdUser == idUser && x.UserProfileImg == true);
        }
        else return null;
    }

    [Obsolete]
    public List<Image> GetImagesByUser(Guid idUser)
    {
        return DbContext.Images.Where(x => x.IdUser == idUser).ToList();
    }

    [Obsolete]
    public void SetBlogTitleImage(Guid idImage, Guid idBlog)
    {
        var oldTitle = DbContext.BlogImages.SingleOrDefault(x => x.IdBlog == idBlog && x.IsTitle == true);

        if (oldTitle != null)
        {
            var img = oldTitle.IdImage;
            DbContext.BlogImages.SingleOrDefault(x => x.IdBlog == idBlog && x.IdImage == img).IsTitle = false;
            DbContext.BlogImages.SingleOrDefault(x => x.IdBlog == idBlog && x.IdImage == idImage).IsTitle = true;
        }
        else
        {
            DbContext.BlogImages.SingleOrDefault(x => x.IdBlog == idBlog && x.IdImage == idImage).IsTitle = true;

        }
    }
    [Obsolete]
    public async Task SaveUser(Guid idImage, Guid idUser)
    {
        var img = DbContext.Images.Any(x => x.Id == idImage);
        if (img)
        {
            DbContext.Images.FindAsync(idImage).Result.IdUser = idUser;
            await DbContext.SaveChangesAsync();
        }
    }
}
}
