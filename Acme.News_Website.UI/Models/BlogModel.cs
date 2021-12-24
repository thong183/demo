using Acme.News_Website.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Models
{
    public class BlogModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; } 
        public Guid IdUser { get; set; }
        public string AuthorName { get; set; }
        public Guid IdCategory { get; set; }
        public string CategoryUrl { get; set; }
        public string CateName { get; set; }
        public decimal RatingPoint { get; set; }
        public int CountLike { get; set; }
        public BlogState State { get; set; }
        public string ImageTitleId { get; set; }
        public List<KeyValuePair<string,string>> Comments { get; set; }
        public List<string> TagName { get; set; }
        public string TitleUrl { get; set; }
        public string AuthorUrl { get; set; }
        public string SubTitle { get; set; }
    }
}
