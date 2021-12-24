using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.News_Website.Authentications
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Summary { get; set; }
        public string NameUrl { get; set; }
        public string UserProfileImage { get; set; }
    }
}
