using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.News_Website.Categories
{
    public interface ICategoryAppService : ICrudAppService<CategoryDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateCategoryDTO>
    {
        List<CategoryDTO> GetOnlyParent();
        Task<List<KeyValuePair<string, string>>> detectParentName(Guid idCate);
        Task<List<CategoryDTO>> GetListNewCategory(int top);
        Task<CategoryDTO> GetCategoryByName(string name);
    }
}
