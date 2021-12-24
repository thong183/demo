using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.News_Website.Authentications
{
    public class token
    {
        public string Access_token { get; set; }
        public DateTime Expire_time { get; set; }
        public string Scope { get; set; }
    }
}
