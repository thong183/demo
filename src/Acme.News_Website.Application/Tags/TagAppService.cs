using Acme.News_Website.Blogs;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Tags
{
    public class TagAppService : CrudAppService<
        Tag,
        TagDTO,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateTagDTO >, ITagAppService
    {
        private readonly ITagRepository _tag;
        private readonly IBlogRepository _blog;
        public TagAppService(IRepository<Tag,Guid> repository, ITagRepository tag, IBlogRepository blog) : base (repository)
        {
            _tag = tag;
            _blog = blog;
        }
        [Authorize(Policy = "admin")]
        public override Task<TagDTO> CreateAsync(CreateUpdateTagDTO input)
        {
            if (_tag.IsExistedTag(input.TagName))
            {
                throw new Exception("Tag name is existed ! please choose another name");
            }
            else
            {
                var res = base.CreateAsync(input);
                res.Result.TagUrl = _blog.UrlFriendly(res.Result.TagName);
                _tag.SaveTagUrl(res.Result.Id, res.Result.TagUrl);
                return res;
            }

        }
        public override Task<PagedResultDto<TagDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var tags =  base.GetListAsync(input);
            
            return tags;
        }
        [Authorize]
        public void RemoveTagFromBlog(Guid idTag, Guid idBlog)
        {
            _tag.RemoveTagFromBlog(idTag, idBlog);
        }
        [Authorize] 
        public void RemoveBlogFromTag(Guid idTag,Guid idBlog)
        {
            _tag.RemoveBlogFromTag(idTag, idBlog);
        }
    }
} 
