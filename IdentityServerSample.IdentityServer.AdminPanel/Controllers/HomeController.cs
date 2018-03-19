using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerSample.IdentityServer.AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using IdentityServerSample.Shared.Constants;

namespace IdentityServerSample.IdentityServer.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var a = User;
            return View();
        }

        [Authorize(ISRoles.Admin)]
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
