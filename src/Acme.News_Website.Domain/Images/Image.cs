using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Images
{
    public class Image : AuditedAggregateRoot<Guid>
    {
        public override Guid Id { get => base.Id; protected set => base.Id = Guid.NewGuid(); } 
        public bool UserProfileImg { get; set; }
        public string ImageUrl { get; set; }
        public Guid IdUser { get; set; }
    }
}
