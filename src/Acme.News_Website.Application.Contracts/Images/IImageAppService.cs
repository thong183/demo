using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.News_Website.Images
{
    public interface IImageAppService : ICrudAppService<ImageDTO,Guid, PagedAndSortedResultRequestDto, CreateUpdateImageOTO>
    {
        List<string> UploadImage(List<IFormFile> images);
        bool SetUserProfileImage(Guid IdImage);
        ImageDTO GetUserProfileImage();
        List<ImageDTO> GetImagesByUser();
        void SetBlogTitleImage(Guid idImage, Guid idBlog);
    }
}
