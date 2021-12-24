using Acme.News_Website.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Acme.News_Website.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(News_WebsiteEntityFrameworkCoreDbMigrationsModule),
        typeof(News_WebsiteApplicationContractsModule)
        )]
    public class News_WebsiteDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
