using Abp.Domain.Repositories;
using Acme.News_Website.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.News_Website.Comments
{
    public interface ICommentRepository : IRepository
    {
        List<KeyValuePair<string, string>> GetCommentsFromBlog(Guid idBlog);
        Task SaveUserIdAsync(Guid UserId, Guid id);

    }
}
