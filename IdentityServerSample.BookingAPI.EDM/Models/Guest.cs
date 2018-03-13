using System.ComponentModel.DataAnnotations;

namespace IdentityServerSample.BookingAPI.EDM.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
