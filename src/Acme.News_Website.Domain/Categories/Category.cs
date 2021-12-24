using Acme.News_Website.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Categories 
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public Guid? IdParent { get; set; } 
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }
}
