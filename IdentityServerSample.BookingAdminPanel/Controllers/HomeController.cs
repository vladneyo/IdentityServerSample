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

        public async Task<IActionResult> Contact()
        {
            ViewData["Message"] = "Your contact page.";

            var disco = await DiscoveryClient.GetAsync(EndpointsConstants.ISHost);
            if (disco.IsError)
            {
                ViewData["APIData"] = "DiscoveryClient error<br>";
                ViewData["APIData"] += disco.Error;
                return View();
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, ISClients.WebAPIClientId, "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(ISApiNames.Api1);

            if (tokenResponse.IsError)
            {
                ViewData["APIData"] = "tokenResponse error";
                ViewData["APIData"] += tokenResponse.Error;
                return View();
            }

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync($"{EndpointsConstants.WebAPIApp}/api/values/");
            if (!response.IsSuccessStatusCode)
            {
                ViewData["APIData"] = response.StatusCode;
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                ViewData["APIData"] = JArray.Parse(content);
            }

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
