using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Services
{
    public interface IApiService
    {
        Task<string> GetApi(string url);
        Task<string> PostApi(object body, string url);
        Task<string> UrlFriendly(string s);
    }
}
