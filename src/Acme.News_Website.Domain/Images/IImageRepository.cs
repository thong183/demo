using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Images
{
    public interface IImageRepository : IRepository
    {
        List<string> UploadImages(List<IFormFile> images, Guid idUser);
        bool SetUserProfileImage(Guid idUser, Guid IdImage);
        Image GetUserProfileImage(Guid idUser);
        string GetImageUrl(Guid Img);
        List<Image> GetImagesByUser(Guid idUser);
        void SetBlogTitleImage(Guid idImage, Guid idBlog);
        Task SaveUser(Guid idImage, Guid idUser);
        Task RemoveImageFromDir(string fileName);
    }
}
