using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Models
{
    public class UserModel
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
