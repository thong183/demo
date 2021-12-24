using Acme.News_Website.Images;
using Acme.News_Website.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Blogs
{
    public class Blog : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdCategory { get; set; }
        public decimal RatingPoint { get; set; }
        public BlogState State { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual HashSet<Image> Images { get; set; }
        public int CountLike { get; set; } = 0;
        public string TitleUrl { get; set; } = "#";
        public string? SubTitle { get; set; }
    }
}
