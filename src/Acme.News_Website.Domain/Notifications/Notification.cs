using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.News_Website.Notifications
{
    public class Notification : AuditedAggregateRoot<Guid>
    {
        public override Guid Id { get => base.Id; protected set => base.Id = Guid.NewGuid(); }
        public Guid IdUser { get; set; }
        public string Content { get; set; }
        public NotiState NotiState { get; set; }
    }
}
