using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Tags
{
    public class TagDTO : AuditedEntityDto<Guid>
    {
        public string TagUrl { get; set; }
        public string TagName { get; set; }
    }
}
