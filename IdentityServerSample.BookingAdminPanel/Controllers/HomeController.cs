using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerSample.BookingAdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using System.Net.Http;
using IdentityServerSample.Shared.Constants;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using IdentityServerSample.BookingAPI.Client;

namespace IdentityServerSample.BookingAdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Contact()
        {
            ViewData["Message"] = "Your contact page.";

            BookingClient<string> client = new BookingClient<string>();

            ViewData["APIData"] = await client.GetAsync("values");

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
    }
}
