using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Acme.News_Website.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class News_WebsiteMigrationsDbContextFactory : IDesignTimeDbContextFactory<News_WebsiteMigrationsDbContext>
    {
        public News_WebsiteMigrationsDbContext CreateDbContext(string[] args)
        {
            News_WebsiteEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<News_WebsiteMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new News_WebsiteMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Acme.News_Website.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
