using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Acme.News_Website
{
    public static class News_WebsiteDtoExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can add extension properties to DTOs
                 * defined in the depended modules.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Object-Extensions
                 */
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserDto, string>("Position");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserDto, string>("Address");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserDto, string>("Summary");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserDto, string>("NameUrl");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserCreateDto, string>("Position");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserCreateDto, string>("Address");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserCreateDto, string>("Summary");
                ObjectExtensionManager.Instance
                 .AddOrUpdateProperty<IdentityUserCreateDto, string>("NameUrl");
            });
        }
    }
}