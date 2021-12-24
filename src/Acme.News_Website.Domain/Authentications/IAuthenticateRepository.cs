using Acme.News_Website.Authentications;
using Acme.News_Website.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public  interface IAuthenticateRepository : IRepository
    {
        Task<token> AuthenticateAsync(string UserName, string PassWord);
        Dictionary<string, string> SignIn(string username, string password);
        Dictionary<string, string> VerifyToken(string token);
        Task<Dictionary<string, string>> GetUserInfo(string token);
        Task<Dictionary<string, object>> SigninFacebook(string token);
        Task<AppUser> CheckExistUserAsync(Dictionary<string, object> infomations);
        Task<string> GetProfilePicture(string id);
        string getAccessToken(string fbToken);
        Task<AppUser> GetUserByName(string name);
    }
}