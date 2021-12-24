using Acme.News_Website.Blogs;
using Acme.News_Website.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Tags
{
    public class Tag : AuditedAggregateRoot<Guid>
    {
        public Tag(string name)
        {
            Id = Guid.NewGuid();
            TagName = name;
        }
        public string TagName { get; set; }
        public string TagUrl { get; set; }
        public virtual List<Blog> Blogs {get;set;}
        public Tag() { }
    }
}
