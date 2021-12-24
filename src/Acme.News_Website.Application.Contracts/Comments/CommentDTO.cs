using System;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Comments
{
    public class CommentDTO : AuditedEntityDto<Guid>
    {
        public Guid IdBlog { get; set; }
        public Guid IdUser { get; set; }
        public DateTime CreateTime { get; set; } 
        public string Cmt { get; set; }
    }
}
