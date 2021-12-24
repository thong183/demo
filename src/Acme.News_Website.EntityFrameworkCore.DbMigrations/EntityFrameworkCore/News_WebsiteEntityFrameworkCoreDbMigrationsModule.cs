using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Acme.News_Website.EntityFrameworkCore
{
    [DependsOn(
        typeof(News_WebsiteEntityFrameworkCoreModule)
        )]
    public class News_WebsiteEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<News_WebsiteMigrationsDbContext>();
        }
    }
}
