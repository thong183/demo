using Microsoft.EntityFrameworkCore;
using Acme.News_Website.Users;
using Acme.News_Website.Blogs;
using Acme.News_Website.Categories;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using Acme.News_Website.Images;
using Acme.News_Website.BlogReviews;
using Acme.News_Website.Trackings;
using Acme.News_Website.Tags;
using Acme.News_Website.User;
using Volo.Abp.ObjectExtending;
using System;
using Acme.News_Website.Comments;
using Acme.News_Website.Notifications;
using Acme.News_Website.BlogImages;

namespace Acme.News_Website.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See News_WebsiteMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class News_WebsiteDbContext : AbpDbContext<News_WebsiteDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images {get;set;}
        public DbSet<BlogReview> BlogReviews { get; set; }
        public DbSet<Tracking> Trackings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
       public DbSet<BlogImage> BlogImages { get; set; }
        
        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside News_WebsiteDbContextModelCreatingExtensions.ConfigureNews_Website
         */

        public News_WebsiteDbContext(DbContextOptions<News_WebsiteDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            /* Configure the shared tables (with included modules) here */
            
            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                
                /* Configure mappings for your additional properties
                 * Also see the News_WebsiteEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureNews_Website method */

            builder.ConfigureNews_Website();
        }
    }
}
