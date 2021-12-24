using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.News_Website.UI.Models;
using Acme.News_Website.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Acme.News_Website.UI.Pages.AuthorListBlog
{
    public class IndexModel : PageModel
    {
        public List<BlogModel> listBlog { get; set; }
        public UserModel Author { get; set; }
        private readonly IApiService _api;
        private readonly IConfiguration _config;
        public IndexModel(IApiService api,IConfiguration config)
        {
            _api = api;
            _config = config;
        }
        public void OnGet(string username)
        {
            var urlAuthor = "Authentication/user-by-name?name=" + username;
            var url = "app/blog/blogs-by-user?username=" + username + "&state=2";
            listBlog = JsonConvert.DeserializeObject<List<BlogModel>>(_api.GetApi(url).Result);
            Author = JsonConvert.DeserializeObject<UserModel>(_api.GetApi(urlAuthor).Result);

        }
        public string getDetailUrl(string TitleUrl)
        {
            var url = _config.GetSection("RedirectUrl:BlogDetail").Value + TitleUrl;
            return url;
        }
    }
}
