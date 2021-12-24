using Acme.News_Website.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.News_Website
{
    [DependsOn(
        typeof(News_WebsiteEntityFrameworkCoreTestModule)
        )]
    public class News_WebsiteDomainTestModule : AbpModule
    {

    }
}