using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerSample.IdentityServer.AdminPanel.Business;
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
    }
}