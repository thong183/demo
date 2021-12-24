using Abp.Domain.Repositories;
using Acme.News_Website.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.News_Website.Tags
{
    public interface ITagRepository : IRepository
    {
        bool IsExistedTag(string TagName);
        List<Blog> GetBlogsByTagName(string tagName);
        bool AddTagsToBlog(List<string> TagNames, Guid idBlog);
        void AddBlogToTag(string TagName, Guid idBlog);
        void RemoveTagsFromBlog(Guid idBlog);
        void RemoveTagFromBlog(Guid idTag, Guid idBlog);
        void RemoveBlogFromTag(Guid idTag, Guid idBlog);
        List<string> GetTagNamesByBlog(Guid idBlog);
        Task SaveTagUrl(Guid id, string url);
    }
}
