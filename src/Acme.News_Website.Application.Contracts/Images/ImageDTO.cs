using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Images
{
    public class ImageDTO : AuditedEntityDto<Guid>
    {
        public bool UserProfileImg { get; set; }
        public string ImageUrl { get; set; }
        public Guid IdUser { get; set; }
    }
}
