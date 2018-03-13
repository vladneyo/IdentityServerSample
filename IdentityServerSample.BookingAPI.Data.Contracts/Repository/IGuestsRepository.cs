using System.Collections.Generic;
using IdentityServerSample.BookingAPI.EDM.Models;

namespace IdentityServerSample.BookingAPI.Data.Contracts.Repository
{
    public interface IGuestsRepository
    {
        List<Guest> GetAll();

        Guest Get(int id);

        Guest Add(Guest guest);

        Guest Update(Guest guest);

        void Delete(int id);
    }
}
