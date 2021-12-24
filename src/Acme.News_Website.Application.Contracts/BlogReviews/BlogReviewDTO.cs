using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.BlogReviews
{
    public class BLogReviewDTO : AuditedEntityDto<Guid>
    {
        public Guid IdBlog { get; set; }
        public Guid IdUser { get; set; }
        
        public int RatingPoint { get; set; }
    }
}
