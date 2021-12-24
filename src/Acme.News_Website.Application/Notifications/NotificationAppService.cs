
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Notifications
{
    public class NotificationAppService : CrudAppService<
        Notification,
        NotificationDTO,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateNotificationDTO
        >, INotificationAppService
    {
        private readonly INotificationRepository _noti;
        public NotificationAppService(IRepository<Notification,Guid> repository, INotificationRepository noti) : base(repository) 
        {
            _noti = noti;
        }
        public override Task<NotificationDTO> GetAsync(Guid id)
        {
            _noti.ChangeNotiState(id);
            return base.GetAsync(id);
        }
        [Authorize]
        public List<KeyValuePair<string,string>> CheckNotification()
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            return _noti.CheckNotification(Guid.Parse(currentUser));
        }
    }
}
