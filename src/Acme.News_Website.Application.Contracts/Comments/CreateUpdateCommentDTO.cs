using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.News_Website.Comments
{
    public class CreateUpdateCommentDTO
    {
        public Guid IdBlog { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        public string Cmt { get; set; }
    }
}
