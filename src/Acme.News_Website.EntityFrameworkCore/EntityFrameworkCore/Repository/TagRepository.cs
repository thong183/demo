using Acme.News_Website.Blogs;
using Acme.News_Website.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class TagRepository : EfCoreRepository<News_WebsiteDbContext, Tag, Guid>, ITagRepository
    {
        private readonly IRepository<Tag, Guid> _tag;
        public TagRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider, IRepository<Tag, Guid> tag) : base(dbContextProvider)
        {
            _tag = tag;
        }

        [Obsolete]
        public bool IsExistedTag(string TagName)
        {
            return DbContext.Tags.Any(x => x.TagName == TagName);
        }

        [Obsolete]
        public List<Blog> GetBlogsByTagName(string tagUrl)
        {
            var tagName = DbContext.Tags.SingleOrDefault(x => x.TagUrl == tagUrl).TagName;
            if (tagName != null)
            {
                var _res = DbContext.Tags.Include("Blogs").SingleOrDefault(x => x.TagName == tagName).Blogs;
                return _res;
            }
            else
            {
                throw new Exception("Not exist tag url");
            }
        }

        [Obsolete]
        public void AddBlogToTag(string TagName, Guid idBlog)
        {
            var tag = DbContext.Tags.Include("Blogs").SingleOrDefault(x => x.TagName == TagName);
            var blog = DbContext.Blogs.SingleOrDefault(x => x.Id == idBlog);
            tag.Blogs.Add(blog);
        }
        [Obsolete]
        public bool AddTagsToBlog(List<string> TagNames, Guid idBlog)
        {
            var _blog = DbContext.Blogs.Include("Tags").SingleOrDefault(x => x.Id == idBlog);
            if (_blog != null)
            {
                foreach (var item in TagNames)
                {
                    var tag = DbContext.Tags.SingleOrDefault(x => x.TagName == item);
                    if (tag == null)
                    {
                        tag = new Tag(item);
                        DbContext.Tags.Add(tag);
                        DbContext.SaveChanges();
                    }
                    _blog.Tags.Add(tag);
                }
                return true;
            }
            else return false;
        }

        [Obsolete]
        public void RemoveTagsFromBlog(Guid idBlog)
        {
            if (DbContext.Blogs.Include("Tags").Any(x => x.Id == idBlog))
            {
                var tags = DbContext.Blogs.Include("Tags").SingleOrDefault(x => x.Id == idBlog).Tags.Select(x => x.Id);
                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        RemoveBlogFromTag(tag, idBlog);
                    }
                }

            }
        }

        [Obsolete]
        public void RemoveTagFromBlog(Guid idTag, Guid idBlog)
        {
            var tag = DbContext.Tags.SingleOrDefault(x => x.Id == idTag);
            DbContext.Blogs.Include("Tags").SingleOrDefault(x => x.Id == idBlog).Tags.Remove(tag);
        }
        [Obsolete]
        public void RemoveBlogFromTag(Guid idTag, Guid idBlog)
        {
            var blog = DbContext.Blogs.SingleOrDefault(x => x.Id == idBlog);
            DbContext.Tags.Include("Blogs").SingleOrDefault(x => x.Id == idTag).Blogs.Remove(blog);
        }

        [Obsolete]
        public List<string> GetTagNamesByBlog(Guid idBlog)
        {
            var tags = DbContext.Blogs.Include("Tags").SingleOrDefault(x => x.Id == idBlog).Tags.Select(x => x.TagName).ToList();
            return tags;
        }
        [Obsolete]
        public async Task SaveTagUrl(Guid id, string url)
        {
            DbContext.Tags.FindAsync(id).Result.TagUrl = url;
            await DbContext.SaveChangesAsync();
        }
    }
}
