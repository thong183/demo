using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.BlogImages
{
    public class BlogImage : AuditedAggregateRoot<Guid>
    {
        public override Guid Id { get => base.Id; protected set => base.Id = Guid.NewGuid(); }
        public Guid IdBlog { get; set; }
        public Guid IdImage { get; set; }
        public bool IsTitle { get; set; } = false;
    }
}
