using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerSample.BookingAPI.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerSample.BookingAPI.Controllers
{
    [Route("api/[controller]")]
    public class GuestsController : Controller
    {
        private readonly IGuestsLogic _guestsLogic;
        
        public GuestsController(IGuestsLogic guestsLogic)
        {
            _guestsLogic = guestsLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_guestsLogic.GetAll());
        }
    }
}