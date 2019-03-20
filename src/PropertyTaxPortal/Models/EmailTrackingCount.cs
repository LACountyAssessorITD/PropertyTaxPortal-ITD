using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public class EmailTrackingCount
    {
        [Key]
        public int emailTrackingCount { get; set; }
    }
}
