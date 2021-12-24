using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.News_Website.Data
{
    /* This is used if database provider does't define
     * INews_WebsiteDbSchemaMigrator implementation.
     */
    public class NullNews_WebsiteDbSchemaMigrator : INews_WebsiteDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}