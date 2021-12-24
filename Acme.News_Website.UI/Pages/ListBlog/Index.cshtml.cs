using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.News_Website.Blogs;
using Acme.News_Website.UI.Models;
using Acme.News_Website.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Acme.News_Website.UI.Pages.ListBlog
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _api;
        public List<BlogModel> listBlogOnCategory;
        public CategoryModel Category { get; set; }
        public string CategoryName { get; set; }
        private readonly IConfiguration _config;
        public IndexModel(IApiService api, IConfiguration config)
        {
            _api = api;
            _config = config;
        }
        public async Task OnGet(string CateUrl)
        {
            var url = "app/blog/by-category-name?cateName="+CateUrl+"&skip=0&top=10";
            var urlCate = "app/category/category-by-name?name=" + CateUrl;
            CategoryName = CateUrl;
            listBlogOnCategory = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
            Category = JsonConvert.DeserializeObject<CategoryModel>(await _api.GetApi(urlCate));
            var urlChild = "app/category/child?idParrent=" + Category.Id.ToString();
            Category.Childs = JsonConvert.DeserializeObject<List<CategoryModel>>(await _api.GetApi(urlChild));
        }
        public string getDetailUrl(string TitleUrl)
        {
            var url = _config.GetSection("RedirectUrl:BlogDetail").Value + TitleUrl;
            return url;
        }
        public async Task GetListBlogByPage(string cateUrl,int page)
        {
            var skip = page * 10;
            var url = "app/blog/by-category-name?cateName=" + cateUrl + "&skip=" + skip + "&top=10";
            listBlogOnCategory = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
        }
        public string getCategoryUrl(string cateUrl)
        {
            var url = _config.GetSection("RedirectUrl:ListBlogCategory").Value + cateUrl ;
            return url;
        }
        public string getAuthorListBlog(string authorUrl)
        {
            return _config.GetSection("RedirectUrl:AuthorListBlog").Value + authorUrl;
        }
    }
}
