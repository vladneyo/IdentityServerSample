using System;
using System.Collections.Generic;
using IdentityServerSample.BookingAPI.Data.Contracts.Repository;
using IdentityServerSample.BookingAPI.EDM.Models;

namespace IdentityServerSample.BookingAPI.Data.Repositories
{
    public class GuestsRepository : IGuestsRepository
    {
        public Guest Add(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Guest Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetAll()
        {
            return new List<Guest>
            {
                new Guest{Id=1, FirstName="Name", LastName="Last"}
            };
        }

        public Guest Update(Guest guest)
        {
            throw new NotImplementedException();
        }
    }
}
