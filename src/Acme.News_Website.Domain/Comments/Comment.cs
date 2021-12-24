using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Comments
{
    public class Comment : AuditedAggregateRoot<Guid>
    {
        public override Guid Id { get => base.Id; protected set => base.Id = Guid.NewGuid(); }
        public Guid IdBlog { get; set; }
        public Guid IdUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string Cmt { get; set; }
    }
}

