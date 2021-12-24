using Acme.News_Website.Authentications;
using Acme.News_Website.Users;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class AuthenticateRepository : EfCoreRepository<News_WebsiteDbContext, AppUser, Guid>, IAuthenticateRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AuthenticateRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider,
                IHttpClientFactory httpClientFactory,
                 IConfiguration configuration
                 ) : base(dbContextProvider)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            
        }
        public async Task<token> AuthenticateAsync(string UserName, string PassWord)
        {
            
            var url = _configuration["AuthServer:Authority"];
            var serverClient = _httpClientFactory.CreateClient();
            var payload = new Dictionary<string, string>
            {
              {"client_id", "News_Website_App"},
              {"client_secret", "1q2w3e*"},
                {"username",UserName },
                {"password",PassWord },
                {"grant_type","password" },
                {"scope","email role News_Website openid" }
             };

            var response= await serverClient.PostAsync(url+"/connect/token", new FormUrlEncodedContent(payload));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = JsonConvert.DeserializeObject<token>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                return responseContent;
            }
            else throw new Exception(response.RequestMessage.ToString());
        }
        public Dictionary<string, string> SignIn(string username,string password)
        {
            var token = AuthenticateAsync(username, password).Result.Access_token;
            var res =  VerifyToken(token);
            res.Add("token", token);
            return res;
        }
        
        public Dictionary<string, string> VerifyToken(string token)
        {
            var receiveValue = new List<string>()
            {
                "email","name","role","openid"
            };
            var handler = new JwtSecurityTokenHandler();
            var lstValue = new Dictionary<string, string>();
            //var jsonToken = handler.ReadToken(token);
            var tokens = handler.ReadToken(token) as JwtSecurityToken;
            var values = tokens.Payload.Claims;
            foreach (var claim in values)
            {
                if(receiveValue.Contains(claim.Type)){
                     lstValue.Add(claim.Type,claim.Value);
                }
            }
            return lstValue;
        }
        public async Task<Dictionary<string, string>> GetUserInfo(string token)
        {
            //var lstValue = new Dictionary<string, string>();
            var client = _httpClientFactory.CreateClient();
            var url = _configuration["AuthServer:Authority"];
            var uri = url + "/connect/userinfo";
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = JsonConvert.DeserializeObject<Dictionary<string,string>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                return responseContent;
            }
            else throw new Exception(response.RequestMessage.ToString());
        }
        public async Task<Dictionary<string, object>> SigninFacebook(string token)
        {
            var userClaims = "id,name,email";
            var client = _httpClientFactory.CreateClient();
            var url = "https://graph.facebook.com/me?fields="+userClaims+"&access_token="+token;
            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                
                return responseContent;
                await client.DeleteAsync(url);
            }
            else throw new Exception(response.RequestMessage.ToString());
        }

        [Obsolete]
        public async Task<AppUser> CheckExistUserAsync(Dictionary<string, object> infomations)
        {
            if(!DbContext.Users.Any(x => x.Name == infomations[infomations.Keys.ElementAt(1)].ToString() && 
                                                x.Email == infomations[infomations.Keys.ElementAt(2)].ToString()))
            {
                var user = new AppUser(infomations[infomations.Keys.ElementAt(1)].ToString(), infomations[infomations.Keys.ElementAt(2)].ToString());
                return user;
            }
            else
            {
                return await DbContext.Users.SingleOrDefaultAsync(x => x.Name == infomations[infomations.Keys.ElementAt(1)].ToString() && 
                                                                    x.Email == infomations[infomations.Keys.ElementAt(2)].ToString());
            }
        }
        public async Task<string> GetProfilePicture(string token)
        {
            var client = _httpClientFactory.CreateClient();
            var url = "https://graph.facebook.com/me?fields=picture&access_token=" + token;
            var response = await client.GetAsync(url);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                var data = responseContent[responseContent.Keys.ElementAt(0)];
                var picProp = data[data.Keys.ElementAt(0)];
                var picUrl = picProp[picProp.Keys.ElementAt(2)].ToString();
                return picUrl;
            }
            else
            {
                return null;
            }

        }
        public static string GetStringFromImage(Image image)
        {
            if (image != null)
            {
                ImageConverter ic = new ImageConverter();
                byte[] buffer = (byte[])ic.ConvertTo(image, typeof(byte[]));
                return Convert.ToBase64String(
                    buffer,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            else
                return null;
        }

        //[Obsolete]
        //public async Task SignOut()
        //{
        //    //var user = HttpContext.User;
        //    if (user?.Identity.IsAuthenticated == true)
        //    {
        //        // delete local authentication cookie
        //        //await HttpContext.SignOutAsync();

        //        // raise the logout event
        //        await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetName()));
        //    }

        //}
        public string getAccessToken(string fbToken)
        {
            var token = "";
            var respose = fbToken;
            var result = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(respose);
            foreach (var res in result)
            {
                if (res.Key == "authResponse")
                {
                    foreach (var item in res.Value)
                    {
                        if (item.Key == "accessToken") token = item.Value;
                    }
                }
            }
            return token;
        }

        [Obsolete]
        public async Task<AppUser> GetUserByName(string name)
        {
            if (await DbContext.Users.AnyAsync(x => x.NameUrl.ToLower() == name.ToLower()))
            {
                return await DbContext.Users.SingleOrDefaultAsync(x => x.NameUrl.ToLower() == name.ToLower());
            }
            else throw new Exception("User is not exist !");
        }
       
    }
}
