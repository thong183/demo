using System;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Notifications
{
    public class NotificationDTO : AuditedEntityDto<Guid>
    {
        public Guid IdUser { get; set; }
        public string Content { get; set; }
        public NotiState NotiState { get; set; }
    }
}
