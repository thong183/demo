using Acme.News_Website.Categories;
using Acme.News_Website.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class CategoryRepository : EfCoreRepository<News_WebsiteDbContext,Category, Guid>,ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider) : base(dbContextProvider) { }

        [Obsolete]
        public bool CheckName(string name)
        {
            return DbContext.Categories.Any(x => x.Name == name);
        }

        [Obsolete]
        public List<Category> GetChild(Guid idParent)
        {
            var cate = DbContext.Categories.Where(x => x.IdParent == idParent).ToList();
            if (cate == null) return null;
            else return cate;
        }
        [Obsolete]
        public List<Category> GetOnlyParent()
        {
            return DbContext.Categories.Where(x => x.IdParent == null).ToList();
        }
        public List<Guid> detectChild(Guid id)
        {
            var cate = DbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (cate == null) throw new Exception("Not Exist Category !");
            List<Guid> lstCategoriesPar = new List<Guid>();
            List<Guid> lstCategoriesPar2 = new List<Guid>();
            lstCategoriesPar.Add(cate.Id);
            //lstCategoriesPar2.Add(cate.Id);
            while (true)
            {
                var lst2 = DbContext.Categories.Where(x => lstCategoriesPar.Contains(x.IdParent.Value)).Select(x => x.Id).ToList();
                if (lst2.Count == 0 || lst2 == null)
                {
                    break;
                }
                else
                {
                    foreach (var item in lst2)
                    {
                        lstCategoriesPar.RemoveRange(0, lstCategoriesPar.Count());
                        lstCategoriesPar.Add(item);
                        lstCategoriesPar2.Add(item);
                    }
                }
            }
            return lstCategoriesPar2;
        }
        [Obsolete]
        public async Task<List<KeyValuePair<string, string>>> detectParentName(Guid idCate)
        {
            var lstParent = new List<KeyValuePair<string,string>>();
            var parent = await DbContext.Categories.AnyAsync(x => x.Id == idCate);
            if (parent)
            {
                var category = await DbContext.Categories.FindAsync(idCate);
                while(category.IdParent != null)
                {
                    category = await DbContext.Categories.FindAsync(category.IdParent);
                    var cate = new KeyValuePair<string, string>(category.Name, category.CategoryUrl);
                    lstParent.Add(cate);
                    
                }
                if(lstParent.Count == 0)
                {
                    var cate = new KeyValuePair<string, string>("","");
                    lstParent.Add(cate);
                }
                return lstParent;
            }
            else
            {
                throw new Exception("Not existed category");
            }
        }

        [Obsolete]
        public async Task<List<Category>> GetListNewCategory( int top)
        {
            return await DbContext.Categories.OrderByDescending(x => x.CreationTime).Take(top).ToListAsync();
        }

        [Obsolete]
        public async Task<Category> GetCategoryByName(string name)
        {
            var CateName = DbContext.Categories.SingleOrDefaultAsync(x => x.CategoryUrl == name).Result.Name;
            if(CateName != null)
            {
                return await DbContext.Categories.SingleOrDefaultAsync(x => x.Name == CateName);
            }
            else
            {
                throw new Exception("Not Exist Category url replace");
            }
            
        }

        [Obsolete]
        public async Task SaveCategoryUrl(Guid id,string url)
        {
            DbContext.Categories.FindAsync(id).Result.CategoryUrl = url;
            await  DbContext.SaveChangesAsync();
        }
    }
}
