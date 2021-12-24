using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.News_Website.Notifications
{
    public interface INotificationAppService : ICrudAppService<NotificationDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateNotificationDTO>
    {
        List<KeyValuePair<string, string>> CheckNotification();
    }
}
