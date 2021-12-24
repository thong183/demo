using Acme.News_Website.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.BlogReviews
{
    public class BlogReview : AuditedAggregateRoot<Guid>
    {
        public override Guid Id { get => base.Id; protected set => base.Id = Guid.NewGuid(); }
        public Guid IdBlog { get; set; }
        public Guid IdUser { get; set; }
        public decimal? RatingPoint { get; set; }
        
    }
}
