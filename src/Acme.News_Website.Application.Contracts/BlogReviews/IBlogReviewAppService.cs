using Acme.News_Website.Blogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.News_Website.BlogReviews
{
    public interface IBlogReviewAppService : ICrudAppService<BLogReviewDTO,Guid, PagedAndSortedResultRequestDto, CreateUpdateBlogReviewDTO>
    {
        
    }
}
