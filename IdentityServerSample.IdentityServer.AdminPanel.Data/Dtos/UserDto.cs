using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public Dictionary<int, Claim> Claims { get; set; }
    }
}
