using Acme.News_Website.BlogReviews;
using Acme.News_Website.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    [Authorize]
    public class BlogReviewRepository : EfCoreRepository<News_WebsiteDbContext, BlogReview, Guid>, IBlogReviewRepository
    {
        private readonly IRepository<BlogReview,Guid> _review;
       public BlogReviewRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider, IRepository<BlogReview, Guid> review) : base(dbContextProvider)
        {
            _review = review;
        }

        [Obsolete]
        public async Task UpdateRatingOnBlog(decimal point,Guid id)
        {
            if (point > 10) point = 10;
            var newPoint = point;
            if ( await DbContext.BlogReviews.AnyAsync(x => x.IdBlog == id))
            {
                DbContext.Blogs.FindAsync(id).Result.RatingPoint = (DbContext.Blogs.FindAsync(id).Result.RatingPoint + newPoint) / 2;
            }
            DbContext.Blogs.FindAsync(id).Result.RatingPoint = newPoint;
            //DbContext.SaveChanges();
        }

        [Obsolete]
        public bool IsExistedBlog(Guid IdBlog)
        {
            return DbContext.Blogs.Any(x => x.Id == IdBlog);
        }

        [Obsolete]
        public async Task DeleteReviewsFromBlog(Guid idBlog)
        {
            var reviews = DbContext.BlogReviews.Where(x => x.IdBlog == idBlog).Select(x => x.Id).ToList();
            if (reviews != null) await _review.DeleteManyAsync(reviews);
        }

        [Obsolete]
        public async Task<bool> IsExistReview(Guid idBlog , Guid IdUser)
        {
            return await DbContext.BlogReviews.AnyAsync(x => x.IdBlog == idBlog && x.IdUser == IdUser) ;
        }
        public List<BlogReview> GetReviewsFromBlog(Guid idBlog)
        {
            return _review.Include("Comments").Where(x => x.IdBlog == idBlog).ToList();
        }

        [Obsolete]
        public async Task<BlogReview> UpdateRating(Guid idBlog,Guid idUser,decimal point)
        
        {
             DbContext.BlogReviews.SingleOrDefaultAsync(x => x.IdBlog == idBlog && x.IdUser == idUser).Result.RatingPoint = point;
            return await DbContext.BlogReviews.SingleOrDefaultAsync(x => x.IdBlog == idBlog && x.IdUser == idUser);
        }
        [Obsolete]
        public async Task SaveUserIdAsync(Guid UserId, Guid id)
        {
            DbContext.BlogReviews.Find(id).IdUser = UserId;
            await DbContext.SaveChangesAsync();
        }
    }
}
