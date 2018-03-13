using AutoMapper;
using IdentityServerSample.BookingAPI.Data.Dtos;
using IdentityServerSample.BookingAPI.EDM.Models;

namespace IdentityServerSample.BookingAPI.Business.AutoMapper.Profiles
{
    public class GuestsProfile : Profile
    {
        public GuestsProfile()
        {
            CreateMap<Guest, GuestDto>();
            CreateMap<GuestDto, Guest>();
        }
    }
}
