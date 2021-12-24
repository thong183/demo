using Acme.News_Website.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class NotificationRepository : EfCoreRepository<News_WebsiteDbContext,Notification, Guid>, INotificationRepository
    {
        private readonly IRepository<Notification, Guid> _noti;
        public NotificationRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider, IRepository<Notification, Guid> noti) : base(dbContextProvider)
        {
            _noti = noti;
        }

        [Obsolete]
        public string SendNotification(Guid idUser,Guid idBlog,string noti)
        {
            var BLogAuthorId = DbContext.Blogs.Find(idBlog).IdUser;
            var sender = DbContext.Users.Find(idUser);
            var blogAuthor = DbContext.Users.Find(BLogAuthorId);
            var notification = new Notification()
            {
                IdUser = blogAuthor.Id,
                Content = sender.UserName+ " " + noti,
                CreationTime = DateTime.UtcNow,
                NotiState = NotiState.Sent
            };
            _noti.InsertAsync(notification);
            return notification.Content;
        }

        [Obsolete]
        public void ChangeNotiState(Guid id)
        {
            DbContext.Notifications.Find(id).NotiState = NotiState.Read;
        }

        [Obsolete]
        public List<KeyValuePair<string,string>> CheckNotification(Guid idUser)
        {
            var listNoti = new List<KeyValuePair<string, string>>();
            var notis = DbContext.Notifications.Where(x => x.IdUser == idUser).ToList();
            foreach (var noti in notis)
            {
                var notiResult = new KeyValuePair<string, string>(noti.Content, noti.CreationTime.ToString());
                listNoti.Add(notiResult);
            }
            return listNoti;
        }
    }
}
