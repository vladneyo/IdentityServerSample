using System.Collections.Generic;
using IdentityServerSample.IdentityServer.EDM.Models;

namespace IdentityServerSample.IdentityServer.AdminPanel.Business
{
    public interface IUsersLogic
    {
        List<ApplicationUser> GetAll();
    }
}
