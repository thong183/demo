using Acme.News_Website.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Categories
{
    public class CategoryDTO : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public int TotalBlog { get; set; } 
        public string CategoryUrl { get; set; }
        public Guid? IdParent { get; set; }
        
    }
}
