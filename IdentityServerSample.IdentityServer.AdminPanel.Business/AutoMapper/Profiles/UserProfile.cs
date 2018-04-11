using AutoMapper;
using IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos;
using IdentityServerSample.IdentityServer.EDM.Models;

namespace IdentityServerSample.IdentityServer.AdminPanel.Business.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.UserName))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
