using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.News_Website.Comments
{
    public interface ICommentAppService : ICrudAppService<CommentDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateCommentDTO>
    {
    }
}

