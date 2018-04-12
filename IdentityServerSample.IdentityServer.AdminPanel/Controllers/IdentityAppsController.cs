using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServerSample.IdentityServer.AdminPanel.Models;
using IdentityServerSample.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerSample.IdentityServer.AdminPanel.Controllers
{
    public class IdentityAppsController : Controller
    {
        readonly ConfigurationDbContext _configCtx;

        public IdentityAppsController(ConfigurationDbContext configCtx)
        {
            _configCtx = configCtx;
        }

        public IActionResult Index()
        {
            var model = new List<IdentityAppViewModel>();
            model.AddRange(_configCtx.ApiResources.Select(x => new IdentityAppViewModel(x.Id.ToString(), x.Name, x.DisplayName, ISAppTypes.ApiResource)));
            model.AddRange(_configCtx.Clients.Select(x => new IdentityAppViewModel(x.Id.ToString(), x.ClientId, x.ClientName, ISAppTypes.Client)));
            return View(model);
        }
    }
}