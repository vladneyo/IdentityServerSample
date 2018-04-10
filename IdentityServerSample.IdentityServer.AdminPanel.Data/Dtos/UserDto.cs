using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IDictionary<int, Claim> Claims { get; set; }
    }
}
