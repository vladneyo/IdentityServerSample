using System.ComponentModel.DataAnnotations;

namespace IdentityServerSample.BookingAPI.EDM.Models
{
    public class LineItem
    {
        [Key]
        public int Id { get; set; }

        public int GuestId { get; set; }

        public int RoomId { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Room Room { get; set; }
    }
}
