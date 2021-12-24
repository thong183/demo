using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.News_Website.Authentications
{
    public class MemberRegisterDTO : AuditedEntityDto<Guid>
    {
        
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
