using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Trackings
{
    public class Tracking : AuditedAggregateRoot<Guid>
    {
        public Tracking(Guid idBlog)
        {
            this.Id = Guid.NewGuid();
            this.IdBlog = idBlog;
            this.SeenCount = 0;
        }
        public Guid IdBlog { get; set; }
        public int SeenCount { get; set; }
    }
}
