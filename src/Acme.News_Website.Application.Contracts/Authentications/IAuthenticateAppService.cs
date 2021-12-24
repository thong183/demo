using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acme.News_Website.Authentications
{
    public interface IAuthenticateAppService
    {
        Task<TokenDTO> AuthenticateAsync(string UserName, string PassWord);
        Dictionary<string, string> VerifyToken(string token);
        Dictionary<string, string> SignIn(string username, string password);
        Task<Dictionary<string, string>> GetUserInfo(string token);
        Task<Dictionary<string, object>> SigninFacebook(string token);
        Task<UserDTO> CheckExistUserAsync(Dictionary<string, object> infomations);
        Task<string> GetProfilePicture(string id);
        string getAccessToken(string fbToken);
        Task<UserDTO> GetUserByName(string name);
    }
}
