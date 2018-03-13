using System.ComponentModel.DataAnnotations;

namespace IdentityServerSample.BookingAPI.EDM.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }
    }
}
