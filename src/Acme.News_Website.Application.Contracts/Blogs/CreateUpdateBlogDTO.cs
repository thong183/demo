
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.News_Website.Blogs
{
    public class CreateUpdateBlogDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string? Subtitle { get; set; }
        //[Required]
        //[DataType(DataType.DateTime)]
        //public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        [Required]
        public Guid IdCategory { get; set; } 
        public List<string> TagName { get; set; }
        public Guid ImageTitleId { get; set; }
        
    }
}
