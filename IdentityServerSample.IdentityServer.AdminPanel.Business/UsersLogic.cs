using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public UserDto Update(UserDto model)
        {
            var user = Mapper.Map<ApplicationUser>(model);
            _ctx.Entry<ApplicationUser>(user).Property(x => x.UserName).IsModified = true;
            _ctx.Attach<ApplicationUser>(user);

            var identityClaims = new List<IdentityUserClaim<string>>();
            model.Claims.ToList().ForEach(x => 
            {
                var claim = new IdentityUserClaim<string>();
                claim.InitializeFromClaim(x.Value);
                claim.Id = x.Key;
                claim.UserId = model.Id;
                identityClaims.Add(claim);
            });
            _ctx.UserClaims.UpdateRange(identityClaims);
            _ctx.SaveChanges();
            return model;
        }
    }
}
