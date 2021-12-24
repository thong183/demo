using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Images
{
    public class ImageAppService : CrudAppService<Image,
        ImageDTO,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateImageOTO
        >, IImageAppService
    {

        private readonly IImageRepository _image;
        public ImageAppService(IRepository<Image,Guid> repository,IImageRepository image) : base(repository) 
        {
            _image = image;
        }
        [Authorize]
        public override async Task<ImageDTO> CreateAsync(CreateUpdateImageOTO input)
        {
            input.UserProfileImg = false;
            var img = await base.CreateAsync(input);
            img.IdUser = Guid.Parse(CurrentUser.FindClaim("sub").Value);
            await _image.SaveUser(img.Id, img.IdUser);
            return img;
        }
        public override Task DeleteAsync(Guid id)
        {
            var img = GetAsync(id);
            _image.RemoveImageFromDir(img.Result.ImageUrl);
            return base.DeleteAsync(id);
        }
        [Authorize]
       public List<string> UploadImage(List<IFormFile> images)
        {
            var res =  _image.UploadImages(images,Guid.Parse(CurrentUser.FindClaim("sub").Value));
            return res;
        }
        [Authorize]
        public bool SetUserProfileImage( Guid IdImage)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            return _image.SetUserProfileImage(Guid.Parse(currentUser), IdImage);
        }
        [Authorize]
        public ImageDTO GetUserProfileImage()
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            var img = _image.GetUserProfileImage(Guid.Parse(currentUser));
            return ObjectMapper.Map<Image, ImageDTO>(img);
        }
        [Authorize]
        public List<ImageDTO> GetImagesByUser()
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            var images = _image.GetImagesByUser(Guid.Parse(currentUser));
            return ObjectMapper.Map<List<Image>, List<ImageDTO>>(images);
        }
        [Authorize]
        public void SetBlogTitleImage(Guid idImage,Guid idBlog)
        {
            _image.SetBlogTitleImage(idImage, idBlog);
        }
        
    }
}
