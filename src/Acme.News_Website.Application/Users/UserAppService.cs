using Acme.News_Website.Blogs;
using Acme.News_Website.EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Acme.News_Website.Users
{
    public class UserAppService : IdentityUserAppService
    {
        private readonly IAuthenticateRepository _auth;
        private readonly IBlogRepository _blog;
        public UserAppService(IdentityUserManager userManager, IIdentityUserRepository userRepository, IIdentityRoleRepository roleRepository, IOptions<IdentityOptions> identityOptions, IAuthenticateRepository auth, IBlogRepository blog) : base(userManager,userRepository,roleRepository,identityOptions) 
        {
            _blog = blog;
            _auth = auth;
        }
        public override Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            input.ExtraProperties["NameUrl"] = _blog.UrlFriendly(input.Name);
            return base.CreateAsync(input);
        }
    }
}
