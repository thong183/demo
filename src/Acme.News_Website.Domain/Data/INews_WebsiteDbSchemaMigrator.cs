using System.Threading.Tasks;

namespace Acme.News_Website.Data
{
    public interface INews_WebsiteDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
