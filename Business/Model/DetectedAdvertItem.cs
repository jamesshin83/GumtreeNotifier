using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Model
{
    public class DetectedAdvertItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DetectedAdvertId { get; set; }
        public DateTime DetectedDate { get; set; }
        public bool IsNotified { get; set; }
        public virtual List<AdvertItem> Adverts { get; set; }
        public virtual SubscriptionRequest SubscriptionRequest { get; set; }
    }
}
