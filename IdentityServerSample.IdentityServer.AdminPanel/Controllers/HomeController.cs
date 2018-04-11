using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityServerSample.IdentityServer.AdminPanel.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.EntityFramework.DbContexts;
using System.Linq;

namespace IdentityServerSample.IdentityServer.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        readonly ConfigurationDbContext _configCtx;

        public HomeController(ConfigurationDbContext configCtx)
        {
            _configCtx = configCtx;
        }

        public async Task<IActionResult> Index()
        {
            var a = User;

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string idToken = await HttpContext.GetTokenAsync("id_token");

            var b = _configCtx.Clients.ToList();

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
