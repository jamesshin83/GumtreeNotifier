using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Model
{
    public class AdvertItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AdvertItemID { get; set; }
        public string GumtreeAdvertId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ThumbnailUrl { get; set; }
        public DateTime AdvertDate { get; set; }
        public String GeneralArea { get; set; }
        public String Suburb { get; set; }
        public DetectedAdvertItem DetectedAdvertItem { get; set; }
    }
}
