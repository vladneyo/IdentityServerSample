using System.Collections.Generic;
using IdentityServerSample.IdentityServer.AdminPanel.Binders;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerSample.IdentityServer.AdminPanel.Models
{
    [ModelBinder(BinderType = typeof(UserEditViewModelBinder))]
    public class UserEditViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public List<ClaimViewModel> Claims { get; set; }

        public int ClaimAmount { get; set; }
    }
}
