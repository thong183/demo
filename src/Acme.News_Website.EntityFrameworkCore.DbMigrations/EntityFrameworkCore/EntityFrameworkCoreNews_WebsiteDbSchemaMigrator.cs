using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.News_Website.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.News_Website.EntityFrameworkCore
{
    public class EntityFrameworkCoreNews_WebsiteDbSchemaMigrator
        : INews_WebsiteDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreNews_WebsiteDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the News_WebsiteMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<News_WebsiteMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}