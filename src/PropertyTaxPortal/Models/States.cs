using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PropertyTaxPortal.Models
{
    public class States
    {
        [Key]
        public string State { get; set; }
    }
}
