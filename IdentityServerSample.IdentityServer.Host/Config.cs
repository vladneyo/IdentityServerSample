using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServerSample.IdentityServer.Host.Extensions;
using IdentityServerSample.Shared.Constants;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerSample.IdentityServer.Host
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
                    ClientName = "Booking Admin",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId, ISIdentityResources.Roles, ISIdentityResources.AppScopes }
                },
                new Client
                {
                    ClientId = ISClients.BookingAPIClientId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(Secrets.BookingAPI.Sha256())},
                    RequireClientSecret = false,
                    AllowedScopes = { ISApiNames.BookingAPI, StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId, ISIdentityResources.Roles, ISIdentityResources.AppScopes }
                },
                new Client
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientId = ISClients.ISAdminPanelClientId,
                    RedirectUris = new string[]{ $"{EndpointsConstants.ISAdminPanel}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{EndpointsConstants.ISAdminPanel}/signout-callback-oidc" },
                    ClientName = "Identity Server Admin",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId, ISIdentityResources.Roles, ISIdentityResources.AppScopes },
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ISApiNames.BookingAPI, "Booking API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource().ToRoles(),
                new IdentityResource().ToAppScopes()
            };
        }
    }
}
