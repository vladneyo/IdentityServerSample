using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityServerSample.IdentityServer.AdminPanel.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServerSample.IdentityServer.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var a = User;

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string idToken = await HttpContext.GetTokenAsync("id_token");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
