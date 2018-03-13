using System.Collections.Generic;
using AutoMapper;
using IdentityServerSample.BookingAPI.Business.Contracts;
using IdentityServerSample.BookingAPI.Data.Contracts.Repository;
using IdentityServerSample.BookingAPI.Data.Dtos;

namespace IdentityServerSample.BookingAPI.Business
{
    public class GuestsLogic : IGuestsLogic
    {
        private readonly IGuestsRepository _guestRepository;

        public GuestsLogic(IGuestsRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public List<GuestDto> GetAll()
        {
            return Mapper.Map<List<GuestDto>>(_guestRepository.GetAll());
        }
    }
}
