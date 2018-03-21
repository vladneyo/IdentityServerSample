using System;
using System.Collections.Generic;
using System.Text;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServerSample.Shared.Constants;

namespace IdentityServerSample.IdentityServer.Host.Extensions
{
    public static class IdentityResourceExtensions
    {
        public static IdentityResource ToRoles(this IdentityResource ir)
        {
            ir.Name = ISIdentityResources.Roles;
            ir.DisplayName = "Your role(s)";
            ir.UserClaims = new[] { JwtClaimTypes.Role };
            return ir;
        }

        public static IdentityResource ToScopes(this IdentityResource ir)
        {
            ir.Name = ISIdentityResources.Scopes;
            ir.DisplayName = "Your user scope";
            ir.UserClaims = new[] { JwtClaimTypes.Scope };
            return ir;
        }
    }
}
