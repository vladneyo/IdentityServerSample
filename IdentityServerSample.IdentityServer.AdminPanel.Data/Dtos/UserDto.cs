using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
