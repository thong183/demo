using Acme.News_Website.UI.Models;
using Acme.News_Website.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {
        private readonly IApiService _api;
        private readonly IConfiguration _config;
        //public List<CategoryModel> categories { get; private set; } 
        public NavBarViewComponent(IApiService api,IConfiguration config)
        {
            _api = api;
            _config = config;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var url = "app/category/only-parent";
            //var res = _api.GetApi(url);
            var categories = JsonConvert.DeserializeObject<List<CategoryModel>>(await _api.GetApi(url));
            foreach(var category in categories)
            {
                category.Childs = GetListChildCategory(category.Id).Result;
                foreach(var child in category.Childs)
                {
                    child.LinkTo = _config.GetSection("RedirectUrl:ListBlogCategory").Value + child.CategoryUrl;
                }
                category.LinkTo = _config.GetSection("RedirectUrl:ListBlogCategory").Value + category.CategoryUrl;
            }
            return View(categories);
        }
        public async Task<List<CategoryModel>> GetListChildCategory(Guid idParent)
        {
            var url = "app/category/child?idParrent=" + idParent.ToString();
            var tmp = JsonConvert.DeserializeObject<List<CategoryModel>>(await _api.GetApi(url));
            return tmp;
        }
        public string redirectHome()
        {
            return _config.GetSection("RedirectUrl : Home").Value;
        }
    }
}
