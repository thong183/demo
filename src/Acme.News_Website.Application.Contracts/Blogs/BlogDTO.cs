using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Blogs
{
    public class BlogDTO : AuditedEntityDto<Guid>
    {
        
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } 
        public Guid IdUser { get; set; }
        public string AuthorName { get; set; }
        public Guid IdCategory { get; set; }
        public string CategoryUrl { get; set; }
        public string CateName { get; set; }
        public decimal RatingPoint { get; set; }
        public int CountLike { get; set; }
        public BlogState State { get; set; }
        public List<KeyValuePair<string,string>> Comments { get; set; }
        public string ImageTitleId { get; set; }
        public string TitleUrl { get; set; }
        public string? SubTitle { get; set; }
        public List<string> TagName { get; set; }
        public string AuthorUrl { get; set; }
    }
}
