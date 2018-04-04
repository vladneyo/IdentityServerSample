using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                .Join(_ctx.UserClaims, u => u.Id, c => c.UserId, (u, c) => new { Id = u.Id, UserName = u.UserName, Claim = c })
                .GroupBy(x => new { x.UserName, x.Id })
                .Select(g => new UserDto
                {
                    Id = g.Key.Id,
                    UserName = g.Key.UserName,
                    Claims = g.Select(x => x.Claim).ToDictionary(x => x.Id, y => y.ToClaim())
                })
                .ToList();
        }

        public UserDto Get(string id)
        {
            return _ctx.Users
                .Join(_ctx.UserClaims, u => u.Id, c => c.UserId, (u, c) => new { Id = u.Id, UserName = u.UserName, Claim = c })
                .GroupBy(x => new { x.UserName, x.Id })
                .Select(g => new UserDto
                {
                    Id = g.Key.Id,
                    UserName = g.Key.UserName,
                    Claims = g.Select(x => x.Claim).ToDictionary(x => x.Id, y => y.ToClaim())
                })
                .First(x => x.Id == id);
        }
    }
}
