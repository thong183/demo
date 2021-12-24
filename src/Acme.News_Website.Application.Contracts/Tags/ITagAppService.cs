using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.News_Website.Tags
{
    public interface ITagAppService : ICrudAppService<TagDTO,Guid, PagedAndSortedResultRequestDto, CreateUpdateTagDTO>
    {
        void RemoveTagFromBlog(Guid idTag,Guid idBlog);
        void RemoveBlogFromTag(Guid idTag, Guid idBlog);
    }
}
