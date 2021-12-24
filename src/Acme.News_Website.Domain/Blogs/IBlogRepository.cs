using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Blogs
{
    public interface IBlogRepository : IRepository
    {
        List<Blog> GetListByCategoryName(string cateName,int skip,int? top);
        List<Blog> GetNewestBlog(int top);
        List<Blog> GetListPopularBlogs(int top, int skip, bool des,Sorting date);
        List<Blog> GetTopLikeBlogs(int top);
        List<Blog> GetAllRepository(BlogState state);
        void AddTagToBlog(Guid idBlog, Guid idTag);
        Task CountSeen(Guid idBlog);
        List<string> GetImagesFromBlog(Guid idBlog);
        List<Blog> GetNewBlogByTime(Sorting time);
        void AddTagsToBlog(Guid IdTag, Guid IdBlog);
        Task ChangeBlogState(Guid id, BlogState state);
        bool ChangeCategory(Guid id, Guid newCate);
        void AddImagesToBlog(Guid idBlog, Guid IdImage);
        string GetTitleImage(Guid id);
        void SetTitleImage(Guid idBlog, Guid idImage);
        bool DeleteImageFromBlog(Guid idImage);
        Task SaveChanges(Guid id, Guid idUser, DateTime createDate);
        bool SendRequest(Guid idUser, Guid idBlog);
        KeyValuePair<string, string> GetAuthor(Guid id);
        string GetCategoryName(Guid id);
        BlogState GetState(Guid id);
        Task AddTracking(Guid idBlog);
        Task<bool> LikePost(Guid idBlog);
        Task<Blog> GetDetailByTitle(string title);
        string UrlFriendly(string s);
        Task SaveTitleUrl(Guid id, string url);
        List<Blog> GetBlogByUser(string username, BlogState state);
    }
}
