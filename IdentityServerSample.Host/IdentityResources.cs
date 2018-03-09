using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServerSample.Host
{
    public class IdentityResources
    {
        public static IEnumerable<IdentityResource> Get()
        {
            return new List<IdentityResource> {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityServer4.Models.IdentityResources.Profile()
            };
        }
    }
}
