using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerSample.IdentityServer.EDM.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerSample.IdentityServer.Host.Services
{
    public class CustomProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            var claims = _userManager.GetClaimsAsync(user).Result;

            context.IssuedClaims.Add(claims.First(x => x.Type == JwtClaimTypes.Scope));
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            context.IsActive = (user != null);

            return Task.CompletedTask;
        }
    }
}
