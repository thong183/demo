using Acme.News_Website.Authenticate;
using Acme.News_Website.Authentications;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Acme.News_Website
{
    [DependsOn(
        typeof(News_WebsiteDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(News_WebsiteApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule)
        )]
    
    
    public class News_WebsiteApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<News_WebsiteApplicationModule>();
            });
            context.Services.AddTransient<IAuthenticateAppService,AuthenticateAppService>();
            context.Services.AddAutoMapperObjectMapper().BuildServiceProvider();

        }
    }
}
