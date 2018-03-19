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
    }
}
