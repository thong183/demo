using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.News_Website.Authentications
{
    public class TokenDTO
    {
        public string Access_token { get; set; }
        public DateTime Expire_time { get; set; }
        public string Scope { get; set; }
    }
}
