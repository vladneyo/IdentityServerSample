using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServerSample.IdentityServer.AdminPanel.Business;
using IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos;
using IdentityServerSample.IdentityServer.AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerSample.IdentityServer.AdminPanel.Controllers
{
    public class UsersController : Controller
    {
        readonly IUsersLogic _usersLogic;

        public UsersController(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        public IActionResult Index()
        {
            return View(_usersLogic.GetAll());
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _usersLogic.Get(id);
            var model = new UserEditViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Claims = user.Claims.Select(x => new ClaimViewModel(x.Key, x.Value.Type, x.Value.Value)).ToList();
            model.ClaimAmount = user.Claims.Count;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            var user = new UserDto();
            user.Id = model.Id;
            user.UserName = model.UserName;
            user.Claims = model.Claims.Select(x => new KeyValuePair<int, Claim>(x.Id, new Claim(x.Type, x.Value))).ToDictionary(x => x.Key, y => y.Value);
            return Redirect("~/Users/");
        }
    }
}