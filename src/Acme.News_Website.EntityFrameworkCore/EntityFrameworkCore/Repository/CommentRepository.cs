using Acme.News_Website.BlogReviews;
using Acme.News_Website.Blogs;
using Acme.News_Website.Comments;
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
    public class CommentRepository : EfCoreRepository<News_WebsiteDbContext,Comment, Guid>, ICommentRepository
    {
        private readonly IBlogReviewRepository _blogReview;
        
        public CommentRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider, IBlogReviewRepository blogReview) : base(dbContextProvider) {
            _blogReview = blogReview;
           
        }

        [Obsolete]
        public List<KeyValuePair<string, string>> GetCommentsFromBlog(Guid idBlog)
        {
            var lst = new List<KeyValuePair<string, string>>();
            var comments = DbContext.Comments.Where(x => x.IdBlog == idBlog).OrderBy(x => x.CreateTime).ToList();
            foreach(var item in comments)
            {
                var user = DbContext.Users.Find(item.IdUser);
                var comment = new KeyValuePair<string, string>(user.UserName, item.Cmt);
                lst.Add(comment);
            }
            return lst;
        }

        [Obsolete]
        public async Task SaveUserIdAsync(Guid UserId,Guid id)
        {
            DbContext.Comments.Find(id).IdUser = UserId;
            await DbContext.SaveChangesAsync();
        }
    }
}
