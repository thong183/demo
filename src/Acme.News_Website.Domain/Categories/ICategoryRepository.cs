using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.News_Website.Categories
{
    public interface ICategoryRepository : IRepository
    {
        
        bool CheckName(string name);
        List<Category> GetChild(Guid idParent);
        List<Category> GetOnlyParent();
        Task<List<KeyValuePair<string, string>>> detectParentName(Guid idCate);
        Task<List<Category>> GetListNewCategory(int top);
        Task<Category> GetCategoryByName(string name);
        List<Guid> detectChild(Guid id);
        Task SaveCategoryUrl(Guid id, string url);
    }
}
