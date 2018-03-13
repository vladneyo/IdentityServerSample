using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerSample.BookingAPI.EDM.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int PrimaryContactId { get; set; }

        

        public virtual Guest PrimaryContact { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }

    }
}
