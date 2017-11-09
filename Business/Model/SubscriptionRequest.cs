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
        public String GumtreeURL { get; set; }
        public String Email { get; set; }
        public DateTime RequestedDate { get; set; }
        public String RequestedBy { get; set; }
        public bool SubscriptionConfirmed { get; set; }
        public DateTime? SubscriptionConfirmedDate { get; set; }
    }
}
