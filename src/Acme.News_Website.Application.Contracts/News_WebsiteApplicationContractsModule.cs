using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Acme.News_Website
{
    [DependsOn(
        typeof(News_WebsiteDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule)
        
    )]
    
    
   
    public class News_WebsiteApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            News_WebsiteDtoExtensions.Configure();
        }
    }
}
