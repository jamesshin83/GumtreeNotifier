using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Model
{
    public class SubscriptionRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SubscriptionId { get; set; }
        public String GumtreeListingURL { get; set; }
        public String Email { get; set; }
        public DateTime RequestedDate { get; set; }
        public String RequestedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? RenewedDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool SubscriptionConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SubscriptionConfirmedDate { get; set; }
    }
}
