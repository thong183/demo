using Acme.News_Website.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.BlogReviews
{
    public interface IBlogReviewRepository : IRepository
    {
        Task UpdateRatingOnBlog(decimal point, Guid id);
        bool IsExistedBlog(Guid IdBlog);
        Task DeleteReviewsFromBlog(Guid idBlog);
        Task<bool> IsExistReview(Guid idBlog, Guid IdUser);
        List<BlogReview> GetReviewsFromBlog(Guid idBlog);
        Task<BlogReview> UpdateRating(Guid idBlog, Guid idUser, decimal point);
        Task SaveUserIdAsync(Guid UserId, Guid id);
    }
}
