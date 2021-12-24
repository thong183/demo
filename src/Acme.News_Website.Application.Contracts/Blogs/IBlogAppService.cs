using Acme.News_Website.BlogReviews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace Acme.News_Website.Blogs
{
    public interface IBlogAppService :
        ICrudAppService<BlogDTO,Guid, PagedAndSortedResultRequestDto,CreateUpdateBlogDTO> 
    {
        List<BlogDTO> GetListByCategoryName(string cateName,int skip,int top);
        List<BlogDTO> GetNewestBlog(int top);
        List<BlogDTO> GetListPopularBlogs(int top, int skip, bool des,Sorting date);
        List<BlogDTO> GetTopLikeBlogs(int top);
        List<BlogDTO> GetBlogsByUser(string username, BlogState state);
        List<BlogDTO> GetAllByStateService(BlogState state);
        List<BlogDTO> GetNewBlogByTimeService(Sorting time);
        Task CountSeenService(Guid id);
        List<BlogDTO> GetBlogsByTagName(string TagName);
        Task ChangeBlogState(Guid id, BlogState state);
        bool ChangeCategory(Guid id, Guid newCate);
        Task<BLogReviewDTO> UpdateRating(Guid idBlog, decimal point);
        bool SendRequest( Guid idBlog);
        Task AddTracking(Guid idBlog);
        Task<bool> LikePostAsync(Guid idBlog);
        Task<BlogDTO> GetDetail(string title);
    }
}
