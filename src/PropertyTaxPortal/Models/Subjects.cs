using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PropertyTaxPortal.Models
{
    public class Subjects
    {
        [Key]
        public string Description { get; set; }

        public string Email { get; set; }
    }
}
