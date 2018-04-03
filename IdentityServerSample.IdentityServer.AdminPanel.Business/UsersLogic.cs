using System.Collections.Generic;
using System.Linq;
using IdentityServerSample.IdentityServer.EDM;
using IdentityServerSample.IdentityServer.EDM.Models;

namespace IdentityServerSample.IdentityServer.AdminPanel.Business
{
    public class UsersLogic : IUsersLogic
    {
        readonly ApplicationDbContext ctx;

        public UsersLogic(ApplicationDbContext applicationDbContext)
        {
            ctx = applicationDbContext;
        }

        public List<ApplicationUser> GetAll()
        {
            return ctx.Users.ToList();
        }
    }
}
