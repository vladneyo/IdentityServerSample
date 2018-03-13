using IdentityServerSample.BookingAPI.EDM.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerSample.BookingAPI.EDM
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
