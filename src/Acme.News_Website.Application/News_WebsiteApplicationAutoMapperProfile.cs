using Acme.News_Website.Authentications;
using Acme.News_Website.BlogReviews;
using Acme.News_Website.Blogs;
using Acme.News_Website.Categories;
using Acme.News_Website.Comments;
using Acme.News_Website.Images;
using Acme.News_Website.Notifications;
using Acme.News_Website.Tags;
using AutoMapper;
using Volo.Abp.Identity;

namespace Acme.News_Website
{
    public class News_WebsiteApplicationAutoMapperProfile : Profile
    {
        public News_WebsiteApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Blog, BlogDTO>();
            CreateMap<CreateUpdateBlogDTO,Blog>();
            CreateMap<Tag, TagDTO>();
            CreateMap<CreateUpdateTagDTO, Tag>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateUpdateCategoryDTO, Category>();
            CreateMap<BlogReview,BLogReviewDTO>();
            CreateMap<CreateUpdateBlogReviewDTO, BlogReview>();
            CreateMap<Image, ImageDTO>();
            CreateMap<CreateUpdateImageOTO,Image>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CreateUpdateCommentDTO, Comment>();
            CreateMap<Notification, NotificationDTO>();
            CreateMap<CreateUpdateNotificationDTO, Notification>();
            CreateMap<token, TokenDTO>();
            CreateMap<IdentityUser, IdentityUserDto>()
            .MapExtraProperties();
        }
    }
}
