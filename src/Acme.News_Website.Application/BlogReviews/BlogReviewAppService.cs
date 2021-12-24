using Acme.News_Website.Blogs;
using Acme.News_Website.Notifications;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace Acme.News_Website.BlogReviews
{
    [Authorize]
    public class BlogReviewAppService : CrudAppService<
        BlogReview,
        BLogReviewDTO,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateBlogReviewDTO>, IBlogReviewAppService
    {
        private readonly IBlogReviewRepository _repo;
        private readonly INotificationRepository _noti;
       
        public BlogReviewAppService(IRepository<BlogReview, Guid> repository, IBlogReviewRepository repo, INotificationRepository noti) : base(repository)
        {
            _repo = repo;
            _noti = noti;
           
        }
        public override async Task<BLogReviewDTO> CreateAsync(CreateUpdateBlogReviewDTO input)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            string noteMessage = "just give a rating on your post";
            _noti.SendNotification(Guid.Parse(currentUser), input.IdBlog, noteMessage);
            if (_repo.IsExistedBlog(input.IdBlog))
            {
                if (await _repo.IsExistReview(input.IdBlog, Guid.Parse(currentUser)))
                {
                    var review = await _repo.UpdateRating(input.IdBlog, Guid.Parse(currentUser), input.RatingPoint);
                    await _repo.UpdateRatingOnBlog(input.RatingPoint, input.IdBlog);
                    return ObjectMapper.Map<BlogReview,BLogReviewDTO>(review);
                }
                else
                {
                    var result = base.CreateAsync(input);
                    result.Result.IdUser = Guid.Parse(currentUser);
                    await _repo.SaveUserIdAsync(Guid.Parse(currentUser), result.Result.Id);
                    await _repo.UpdateRatingOnBlog(input.RatingPoint,input.IdBlog);
                    return result.Result;
                }
            }
            else throw new Exception("BLog is not Exist ! Please try again");
        }
    }
}
