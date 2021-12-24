using Acme.News_Website.User;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Acme.News_Website.EntityFrameworkCore
{
    public static class News_WebsiteEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            News_WebsiteGlobalFeatureConfigurator.Configure();
            News_WebsiteModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE News_WebsiteModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:

                     ObjectExtensionManager.Instance
                         .MapEfCoreProperty<IdentityUser, string>(
                             "MyProperty",
                             (entityBuilder, propertyBuilder) =>
                             {
                                 propertyBuilder.HasMaxLength(128);
                             }
                         );

                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
                 */
                ObjectExtensionManager.Instance
                        .MapEfCoreProperty<IdentityUser, string>(
                            "Address",
                            (entityBuilder, propertyBuilder) =>
                            {
                                propertyBuilder.HasMaxLength(128);
                            }
                        );
                ObjectExtensionManager.Instance
                        .MapEfCoreProperty<IdentityUser, string>(
                            "Position",
                            (entityBuilder, propertyBuilder) =>
                            {
                                propertyBuilder.HasMaxLength(128);
                            }
                        );
                ObjectExtensionManager.Instance
                        .MapEfCoreProperty<IdentityUser, string>(
                            "Summary",
                            (entityBuilder, propertyBuilder) =>
                            {
                                propertyBuilder.HasMaxLength(128);
                            }
                        );
                ObjectExtensionManager.Instance
                        .MapEfCoreProperty<IdentityUser, string>(
                            "NameUrl",
                            (entityBuilder, propertyBuilder) =>
                            {
                                propertyBuilder.HasMaxLength(128);
                            }
                        );
                
            });
        }
    }
}
