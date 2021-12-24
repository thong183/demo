using Acme.News_Website.Blogs;
using Acme.News_Website.SearchEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class SearchEngineRepository : ISearchEngineRepository
    {
        private readonly IRepository<Blog,Guid> _blog;
        public SearchEngineRepository(IRepository<Blog,Guid> blog) 
        {
           _blog = blog;
        }

    }
}
