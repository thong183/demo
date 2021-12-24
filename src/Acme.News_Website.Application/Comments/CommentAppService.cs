

using Acme.News_Website.BlogReviews;
using Acme.News_Website.Notifications;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Comments
{
    [Authorize]
    public class CommentAppService: CrudAppService<Comment, CommentDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateCommentDTO>,
        ICommentAppService
    {
        private readonly INotificationRepository _noti;
        private readonly ICommentRepository _comment;
        public CommentAppService(IRepository<Comment,Guid> repository, INotificationRepository noti, ICommentRepository comment) : base(repository) 
        {
            _noti = noti;
            _comment = comment;
        }
        [Authorize]
        public override Task<CommentDTO> CreateAsync(CreateUpdateCommentDTO input)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            string noteMessage = "just comment on your post";

            _noti.SendNotification(Guid.Parse(currentUser), input.IdBlog, noteMessage);
            var result = base.CreateAsync(input);
            result.Result.IdUser = Guid.Parse(currentUser);
            _comment.SaveUserIdAsync(Guid.Parse(currentUser), result.Result.Id);
            return result;
        }

    }
}
