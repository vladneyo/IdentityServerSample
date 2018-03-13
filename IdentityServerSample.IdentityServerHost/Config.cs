using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServerSample.Shared.Constants;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerSample.IdentityServerHost
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientId = ISClients.BookingAdminPanelClientId,
                    RedirectUris = new string[]{ $"{EndpointsConstants.BookingAdminPanel}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{EndpointsConstants.BookingAdminPanel}/signout-callback-oidc" },
                    ClientName = "mvc",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId }
                },
                new Client
                {
                    ClientId = ISClients.BookingAPIClientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(Secrets.BookingAPI.Sha256())},
                    RequireClientSecret = false,
                    AllowedScopes = { ISApiNames.BookingAPI, StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ISApiNames.BookingAPI, "WebAPI App")
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
