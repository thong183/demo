using Volo.Abp.Modularity;

namespace Acme.News_Website
{
    [DependsOn(
        typeof(News_WebsiteApplicationModule),
        typeof(News_WebsiteDomainTestModule)
        )]
    public class News_WebsiteApplicationTestModule : AbpModule
    {

    }
}