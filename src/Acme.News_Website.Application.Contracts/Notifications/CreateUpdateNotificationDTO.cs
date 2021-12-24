using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.News_Website.Notifications
{
    public class CreateUpdateNotificationDTO
    {
        public Guid IdUser { get; set; }
        public string Content { get; set; }
        public NotiState NotiState { get; set; } = NotiState.Sent;
    }
}

