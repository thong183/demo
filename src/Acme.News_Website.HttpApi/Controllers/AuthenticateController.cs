using Acme.News_Website.Authentications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Acme.News_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateAppService _auth;
        private readonly IIdentityUserAppService _user;
        public AuthenticationController(IAuthenticateAppService auth, IIdentityUserAppService user)
        {
            _auth = auth;
            _user = user;
        }

        [HttpPost("SingIn")]
       
        public IActionResult Signin(string userName, string passWord)
        {
            var res = _auth.SignIn(userName, passWord);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Login Fail");
            }
        }
        
        [HttpPost("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(string token)
        {
            var res = await _auth.GetUserInfo(token);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Invalid Token");
            }
        }
        [HttpPost("SigninFacebook")]
        public async Task<IActionResult> SigninFacebook(string token)
        {
            try
            {
                var res = await _auth.SigninFacebook(token);
                var user = await _auth.CheckExistUserAsync(res);
                var Iuser = new IdentityUserCreateDto()
                {
                    Name = user.Name,
                    Email = user.Email
                };
                await _user.CreateAsync(Iuser);
                if (res != null)
                {
                    return Ok(res);
                }

                else
                {
                    return BadRequest("Invalid Token");
                }
            }
            catch(Exception ex)
            {

                throw ex;
            }


        }
        
        [HttpGet("GetUserFacebookProfile")]
        public async Task<IActionResult> GetUserFacebookProfile(string id)
        {
            var res = await _auth.GetProfilePicture(id);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return Ok("Not Exist !");
            }
        }
        [HttpGet("user-by-name")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var res = await _auth.GetUserByName(name);
            if(res != null)
            {
                return Ok(res);
            }
            else
            {
                throw new Exception("Fail to get User");
            }
        }
    }
}

