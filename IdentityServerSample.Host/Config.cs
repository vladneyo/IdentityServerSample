using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServerSample.Shared.Constants;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerSample.Host
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientId = ISClients.MVCClientId,
                    RedirectUris = new string[]{ $"{EndpointsConstants.MVCApp}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{EndpointsConstants.MVCApp}/signout-callback-oidc" },
                    ClientName = "mvc",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId }
                },
                new Client
                {
                    ClientId = ISClients.WebAPIClientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256())},
                    RequireClientSecret = false,
                    AllowedScopes = { ISApiNames.Api1, StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ISApiNames.Api1, "WebAPI App")
            };
        }

        public static List<TestUser> GetUsers()
        {
            return TestUsers.Users;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }
    }
}
