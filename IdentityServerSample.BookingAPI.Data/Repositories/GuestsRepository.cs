using System;
using System.Linq;
using System.Collections.Generic;
using IdentityServerSample.BookingAPI.Data.Contracts.Repository;
using IdentityServerSample.BookingAPI.EDM;
using IdentityServerSample.BookingAPI.EDM.Models;

namespace IdentityServerSample.BookingAPI.Data.Repositories
{
    public class GuestsRepository : IGuestsRepository
    {
        private BookingContext ctx;
        public GuestsRepository(BookingContext context)
        {
            ctx = context;
        }

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
            return ctx.Guests.ToList();
        }

        public Guest Update(Guest guest)
        {
            throw new NotImplementedException();
        }
    }
}
