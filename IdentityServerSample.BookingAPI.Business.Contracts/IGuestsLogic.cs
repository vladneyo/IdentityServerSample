using System.Collections.Generic;
using IdentityServerSample.BookingAPI.Data.Dtos;

namespace IdentityServerSample.BookingAPI.Business.Contracts
{
    public interface IGuestsLogic
    {
        List<GuestDto> GetAll();

    }
}
