using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerSample.Host
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
                //new Client
                //{
                //    AccessTokenType = AccessTokenType.Jwt,
                //    ClientId = "123",
                //    RedirectUris = new string[]{ "" },
                //    ClientName = "api1",
                //    ClientSecrets = { new Secret("secret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //},
                new Client
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientId = "mvc",
                    RedirectUris = new string[]{ "http://localhost:52012/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:52012/signout-callback-oidc" },
                    ClientName = "mvc",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId }
                }
            };
        }
    }
}
