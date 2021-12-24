using Acme.News_Website.Blogs;
using Acme.News_Website.UI.Models;
using Acme.News_Website.UI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly IApiService _api;
        private readonly IConfiguration _config;
        public List<BlogModel> AllBlog { get; private set; }
        public string stringTest {get;set; }
        public IndexModel( IApiService api, IConfiguration config)
        {
            _api = api;
            _config = config;
        }

        public async Task OnGetAsync()
        {
            var url = "app/blog";
            var tmp = JsonConvert.DeserializeObject<Dictionary<string, object>>(await _api.GetApi(url));
            var tmp2 = tmp.ElementAt(1).Value;
            var json = JsonConvert.SerializeObject(tmp2);
            AllBlog = JsonConvert.DeserializeObject<List<BlogModel>>(json);
        }
        public string getDetailUrl(string TitleUrl)
        {
            var url = _config.GetSection("RedirectUrl:BlogDetail").Value+ TitleUrl;
            return url;
        }
        public string getCategoryUrl(string cateUrl)
        {
            var url = _config.GetSection("RedirectUrl:ListBlogCategory").Value + cateUrl;
            return url;
        }
        public async Task<List<BlogModel>> GetTopNewestBlog(int top)
        {
            var url = "app/blog/newest-blog?top="+top.ToString();
            var lstblog = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
            return lstblog;
        }
        public async Task<List<CategoryModel>> GetListCategory()
        {
            var url = "app/category/only-parent";
            var tmp = JsonConvert.DeserializeObject<List<CategoryModel>>(await _api.GetApi(url));
            return tmp;
        }
        public async Task<List<CategoryModel>> GetListChildCategory(Guid idParent)
        {
            var url = "app/category/child?idParrent=" + idParent.ToString();
            var tmp = JsonConvert.DeserializeObject<List<CategoryModel>>(await _api.GetApi(url));
            return tmp;
        }
        public async Task<List<BlogModel>> GetListBlogByCategory(string CateUrl)
        {
            var url = "app/blog/by-category-name?cateName="+CateUrl+"&skip=0&top=2";
            var tmp = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
            return tmp;
        }
        public async Task<List<BlogModel>> GetTopHotBlog(int top)
        {
            var url = "app/blog/top-like-blogs?top=" + top.ToString();
            var blogs = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
            return blogs;
        }
        public async Task<List<BlogModel>> GetPopularBlogsOnDay(int top,int skip)
        {
            var url = "app/blog/popular-blogs?top="+top.ToString()+"&skip="+skip.ToString()+"&des=true&date=0";
            return JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
        }
        public async Task<List<BlogModel>> GetPopularBlogsOnMonth(int top, int skip)
        {
            var url = "app/blog/popular-blogs?top=" + top.ToString() + "&skip=" + skip.ToString() + "&des=true&date=1";
            return JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
        }
        public async Task<List<BlogModel>> GetPopularBlogsOnYear(int top, int skip)
        {
            var url = "app/blog/popular-blogs?top=" + top.ToString() + "&skip=" + skip.ToString() + "&des=true&date=2";
            
            return JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
        }
        public async Task changeText(string s)
        {
            stringTest = await _api.UrlFriendly(s.ToLower());
        }
    }
}
