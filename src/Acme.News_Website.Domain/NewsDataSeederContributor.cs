using Abp.Dependency;
using Acme.News_Website.Blogs;
using Acme.News_Website.Categories;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website
{
    public class NewsDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _repo;
        public NewsDataSeederContributor(IRepository<Category, Guid> repo)
        {
            _repo = repo;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _repo.GetCountAsync() <= 0)
            {
                await _repo.InsertAsync(
                    new Category
                    {
                        Name = "Football",
                        
                    },
                    autoSave: true
                ) ;
            }
        }
    }
}
