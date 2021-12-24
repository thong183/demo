using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Notifications
{
    public interface INotificationRepository : IRepository
    {
        string SendNotification(Guid idUser, Guid idBlog, string noti);
        void ChangeNotiState(Guid id);
        List<KeyValuePair<string, string>> CheckNotification(Guid idUser);
    }
}
