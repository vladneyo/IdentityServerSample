using System.Collections.Generic;
using System.Linq;
using IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos;
using IdentityServerSample.IdentityServer.EDM;
using IdentityServerSample.IdentityServer.EDM.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerSample.IdentityServer.AdminPanel.Business
{
    public class UsersLogic : IUsersLogic
    {
        readonly ApplicationDbContext _ctx;

        public UsersLogic(ApplicationDbContext applicationDbContext)
        {
            _ctx = applicationDbContext;
        }

        public List<UserDto> GetAll()
        {
            return _ctx.Users
                .Join(_ctx.UserClaims, u => u.Id, c => c.UserId, (u, c) => new { UserName = u.UserName, Claim = c })
                .GroupBy(x => x.UserName)
                .Select(g => new UserDto
                    {
                        UserName = g.Key,
                        Claims = g.Select(x => x.Claim.ToClaim()).ToList()
                    })
                .ToList();
        }
    }
}
