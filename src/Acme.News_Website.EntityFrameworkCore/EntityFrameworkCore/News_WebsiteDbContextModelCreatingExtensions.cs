using Abp.Authorization.Users;
using Acme.News_Website.BlogImages;
using Acme.News_Website.BlogReviews;
using Acme.News_Website.Blogs;
using Acme.News_Website.Categories;
using Acme.News_Website.Comments;
using Acme.News_Website.Images;
using Acme.News_Website.Notifications;
using Acme.News_Website.Tags;
using Acme.News_Website.Trackings;
using Acme.News_Website.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;


namespace Acme.News_Website.EntityFrameworkCore
{
    public static class News_WebsiteDbContextModelCreatingExtensions
    {
        public static void ConfigureNews_Website(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(News_WebsiteConsts.DbTablePrefix + "YourEntities", News_WebsiteConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
            builder.Entity<Blog>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "Blogs", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Title).IsRequired().HasMaxLength(128);
                b.HasOne<Category>().WithMany().HasForeignKey(x => x.IdCategory).IsRequired();
                //b.HasOne<AppUser>().WithMany().HasForeignKey(x => x.IdUser).IsRequired();

            });
            builder.Entity<Category>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "Categories", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.IdParent).IsRequired(false);
                b.HasMany(x => x.Children).WithOne(x => x.Parent).HasForeignKey(x => x.IdParent);
                
            });
            builder.Entity<BlogReview>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "BlogReviews", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.IdBlog).IsRequired().HasMaxLength(128);
                b.Property(x => x.IdUser).IsRequired().HasMaxLength(128);
                b.HasOne<Blog>().WithMany().HasForeignKey(x => x.IdBlog).IsRequired();
                //b.HasOne<AppUser>().WithMany().HasForeignKey(x => x.IdUser).IsRequired();
                
            });
            builder.Entity<Tracking>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "Trackings", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.HasOne<Blog>().WithMany().HasForeignKey(x => x.IdBlog).IsRequired();
            });
            builder.Entity<Image>(b => 
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "Images", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.UserProfileImg);
                b.Property(x => x.ImageUrl);
                //b.HasOne<Blog>().WithMany().HasForeignKey(x => x.IdBlog).IsRequired();
            });
            builder.Entity<Tag>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "Tags", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.TagName).IsRequired().HasMaxLength(128);
                b.HasMany(x => x.Blogs).WithMany(x => x.Tags);
                //b.HasOne<AppUser>().WithMany().HasForeignKey(x => x.IdUser);

            });
            builder.Entity<Comment>(b =>
           {
               b.ToTable(News_WebsiteConsts.DbTablePrefix + "Comments", News_WebsiteConsts.DbSchema);
               b.ConfigureByConvention();
               b.HasOne<Blog>().WithMany().HasForeignKey(x => x.IdBlog).IsRequired();
           });
            builder.Entity<Notification>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "Notifications", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention();
                //b.HasOne<AppUser>().WithMany().HasForeignKey(x => x.IdUser);
            });
            builder.Entity<BlogImage>(b =>
            {
                b.ToTable(News_WebsiteConsts.DbTablePrefix + "BlogImages", News_WebsiteConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Image>().WithMany().HasForeignKey(x => x.IdImage);
                b.HasOne<Blog>().WithMany().HasForeignKey(x => x.IdBlog);
            });
            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable("AppUser", News_WebsiteConsts.DbSchema);
            //    b.ConfigureByConvention();



            //});
        }
    }
}