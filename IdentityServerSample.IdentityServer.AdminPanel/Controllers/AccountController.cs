using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerSample.IdentityServer.AdminPanel.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}