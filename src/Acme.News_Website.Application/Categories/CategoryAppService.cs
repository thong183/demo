using Acme.News_Website.Blogs;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Categories
{
    public class CategoryAppService :
        CrudAppService<Category, CategoryDTO, Guid, PagedAndSortedResultRequestDto, CreateUpdateCategoryDTO>,
        ICategoryAppService
    {
        private readonly ICategoryRepository _category;
        private readonly IBlogRepository _blog;
        public CategoryAppService(IRepository<Category, Guid> repository, ICategoryRepository category, IBlogRepository blog) : base(repository)
        {
            _category = category;
            _blog = blog;
        }
        [Authorize(Policy = "admin")]
        public override Task<CategoryDTO> CreateAsync(CreateUpdateCategoryDTO input)
        {
            if (_category.CheckName(input.Name)) throw new Exception("Category Name has been existed ! Please try another");
            else
            {
                var _res = base.CreateAsync(input);
                _res.Result.CategoryUrl = _blog.UrlFriendly(_res.Result.Name);
                _category.SaveCategoryUrl(_res.Result.Id, _res.Result.CategoryUrl);
                return _res;
            }
        }
        public List<CategoryDTO> GetOnlyParent()
        {
            var cate = _category.GetOnlyParent();
            return ObjectMapper.Map<List<Category>, List<CategoryDTO>>(cate);
        }

        public override async Task<PagedResultDto<CategoryDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {

            var _res = await base.GetListAsync(input);
            List<CategoryDTO> lstDTO = new List<CategoryDTO>();
            foreach (var item in _res.Items)
            {
                item.TotalBlog = _blog.GetListByCategoryName(item.CategoryUrl, 0, null).Count;
            }
            //PagedResultDto<CategoryDTO> result = new PagedResultDto<CategoryDTO>(lstDTO.Count, lstDTO);
            return _res;
        }
        public async Task<int> GetTotalPage(int MaxResult)
        {
            if (MaxResult <= 0)
            {
                return 0;
            }
            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto();

            var _res = _category.GetOnlyParent();

            int totalPage = _res.Count % MaxResult == 0 ? _res.Count / MaxResult : _res.Count / MaxResult + 1;
            return totalPage;
        }
        public override Task<CategoryDTO> GetAsync(Guid id)
        {
            var res = base.GetAsync(id);
            res.Result.TotalBlog = _blog.GetListByCategoryName(res.Result.CategoryUrl, 0, null).Count;
            return res;
        }
        public List<Category> GetChild(Guid idParrent)

        {
            return _category.GetChild(idParrent);
        }
        public async Task<List<KeyValuePair<string, string>>> detectParentName(Guid idCate)
        {
            return await _category.detectParentName(idCate);
        }
        public async Task<List<CategoryDTO>> GetListNewCategory(int top)
        {
            var cates = ObjectMapper.Map<List<Category>, List<CategoryDTO>>(await _category.GetListNewCategory(top));
            foreach (var cate in cates)
            {
                cate.TotalBlog = _blog.GetListByCategoryName(cate.CategoryUrl, 0, null).Count;
            }
            return cates;
        }
        public async Task<CategoryDTO> GetCategoryByName(string name)
        {
            return ObjectMapper.Map<Category, CategoryDTO>(await _category.GetCategoryByName(name));
        }
        [Authorize(Policy = "admin")]
        public override Task<CategoryDTO> UpdateAsync(Guid id, CreateUpdateCategoryDTO input)
        {
            var lstChild = _category.detectChild(id);
            foreach (var item in lstChild)
            {
                if (input.IdParent == item)
                {
                    throw new Exception("Cannot set parent for current category by its own child node");
                }
            }
            return base.UpdateAsync(id, input);
        }
    }
}
