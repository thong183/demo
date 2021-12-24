using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.News_Website.Blogs;
using Acme.News_Website.UI.Models;
using Acme.News_Website.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Acme.News_Website.UI.Pages.Detail
{
    public class IndexModel : PageModel
    {
        public BlogModel blog;
        public string token { get; set; }
        private readonly IApiService _api;
        private readonly IConfiguration _config;
        public IndexModel(IApiService api, IConfiguration config)
        {
            _api = api;
            _config = config;
        }
        public void OnGet(string urlDetail)
        {
            var url = "app/blog/detail?title=" + urlDetail;
            var response = _api.GetApi(url).Result;
            blog = JsonConvert.DeserializeObject<BlogModel>(response);
        }
        public async Task<UserModel> SignInFacebook(string token)
        {
            //var token = getAccessToken();
            var url = "authentication/signinFacebook";
            var res = await _api.PostApi(token,url);
            return JsonConvert.DeserializeObject<UserModel>(res);       
        }
        public async Task<bool> LikeBlog(Guid idBlog)
        {
            var url = "app/blog/like-post";
            var res = await _api.PostApi(idBlog,url);
            return JsonConvert.DeserializeObject<bool>(res);
        }
        public async Task OnPost()
        {
            var rating = Request.Form["Rating"];
            //var user = SignInFacebook();
            //var body = new RatingModel()
            //{
            //    IdBlog = Guid.Parse(Request.Form["blog"].ToString()),
            //    IdUser = Guid.Parse(user.Result.Id),
            //    RatingPoint = int.Parse(rating.ToString())
            //};
            //var url = "app/blog-review";
            //await _api.PostApi(body, url);
            //return JsonConvert.DeserializeObject<RatingModel>(res);
        }
        public List<BlogModel> GetBlogsFromCateName(string cateUrl)
        {
            var url = "app/blog/by-category-name?cateName=" + cateUrl+"&skip=0&top=5";
            var response = _api.GetApi(url).Result;
            var blogs = JsonConvert.DeserializeObject<List<BlogModel>>(response);
            
            return blogs;
        }
        public string RedirectHome()
        {
            return _config.GetSection("RedirectUrl:Home").Value;
        }
        public List<KeyValuePair<string, string>> detectParentName(Guid id)
        {
            var lst = new List<string>();
            var url = "app/category/detect-parent-name?idCate=" + id.ToString();
            var response = _api.PostApi(id,url).Result ;
            return JsonConvert.DeserializeObject<List<KeyValuePair<string,string>>>(response);
        }
        public string getCategoryUrl(string cateUrl)
        {
            var url = _config.GetSection("RedirectUrl:ListBlogCategory").Value + cateUrl;
            return url;
        }
        public async Task<List<BlogModel>> GetTopNewestBlog(int top)
        {
            var url = "app/blog/newest-blog?top=" + top.ToString();
            var lstblog = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
            return lstblog;
        }
        public async Task<List<BlogModel>> GetTopHotBlog(int top)
        {
            var url = "app/blog/top-like-blogs?top=" + top.ToString();
            var blogs = JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
            return blogs;
        }
        public async Task<List<BlogModel>> GetPopularBlogsOnMonth(int top, int skip)
        {
            var url = "app/blog/popular-blogs?top=" + top.ToString() + "&skip=" + skip.ToString() + "&des=true&date=1";
            return JsonConvert.DeserializeObject<List<BlogModel>>(await _api.GetApi(url));
        }
        public string getDetailUrl(string TitleUrl)
        {
            var url = _config.GetSection("RedirectUrl:BlogDetail").Value + TitleUrl;
            return url;
        }
        public async Task<List<CategoryModel>> getNewstCategories(int top)
        {
            var url = "app/category/new-category?top=" + top.ToString();
            return JsonConvert.DeserializeObject<List<CategoryModel>>(await _api.GetApi(url));
        }
        public string getAuthorListBlog(string authorUrl)
        {
            return _config.GetSection("RedirectUrl:AuthorListBlog").Value + authorUrl;
        }
        //public string getAccessToken( string fbToken)
        //{
        //    var token = "";
        //    var respose = fbToken;
        //    var result = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(respose);
        //    foreach (var res in result)
        //    {
        //        if (res.Key == "authResponse")
        //        {
        //            foreach (var item in res.Value)
        //            {
        //                if (item.Key == "accessToken") token = item.Value;
        //            }
        //        }
        //    }
        //    return token;
        //}
    }
}
