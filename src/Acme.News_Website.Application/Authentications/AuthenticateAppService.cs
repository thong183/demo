
using Acme.News_Website.Authentications;
using Acme.News_Website.EntityFrameworkCore.Repository;
using Acme.News_Website.Images;
using Acme.News_Website.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.News_Website.Authenticate
{
    public class AuthenticateAppService : IAuthenticateAppService
    {
        private readonly IAuthenticateRepository _auth;
        private readonly IImageRepository _image;
        //private readonly IRepository<AppUser, Guid> _userRepo;
        //private readonly IdentityUserManager _identityUserManager;
        //private readonly IdentityUser _identityUser;
        public AuthenticateAppService(IAuthenticateRepository auth, IImageRepository image)//, IRepository<AppUser, Guid> userRepo, IdentityUserManager identityUserManager, IdentityUser identityUser) 
        {
            _auth = auth;
            _image = image;
            //_userRepo = userRepo;
            //_identityUserManager = identityUserManager;
            //_identityUser = identityUser;
        }

        public async Task<TokenDTO> AuthenticateAsync(string UserName, string PassWord)
        {
            var token = await _auth.AuthenticateAsync(UserName, PassWord);
            var result = new TokenDTO()
            {
                Access_token = token.Access_token,
                Expire_time = token.Expire_time,
                Scope = token.Scope
            };
            return result;
        }
        public Dictionary<string, string> VerifyToken(string token)
        {
            return _auth.VerifyToken(token);
        }
        public Dictionary<string, string> SignIn(string username, string password)
        {
            return _auth.SignIn(username, password);
        } 
        public Task<Dictionary<string, string>> GetUserInfo(string token)
        {
            return _auth.GetUserInfo(token);
        }
        public Task<Dictionary<string, object>> SigninFacebook(string token)
        {
            return _auth.SigninFacebook(token);
        }
        public async Task<UserDTO> CheckExistUserAsync(Dictionary<string, object> infomations)
        {
            var res = await _auth.CheckExistUserAsync(infomations);
            var user = new UserDTO()
            {
                Name = res.Name,
                Email = res.Email
            };
            return user;
        }
        //public async Task<MemberRegisterDTO> Register(MemberRegisterDTO user)
        //{

        //}
        public async Task<string> GetProfilePicture(string id)
        {
            return await _auth.GetProfilePicture(id);
        }
        public string getAccessToken(string fbToken)
        {
            return _auth.getAccessToken(fbToken);
        }
        public async Task<UserDTO> GetUserByName(string name)
        {
            var user = await _auth.GetUserByName(name);
            var User = new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Position = user.Position,
                Summary = user.Summary,
                NameUrl = user.NameUrl,
                UserProfileImage = _image.GetUserProfileImage(user.Id).ImageUrl
            };
            return User;
        }
    }
}
