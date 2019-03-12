using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace PropertyTaxPortal.Models
{
    public class RefCode
    {
        [Key]
        [Column("RefCode")]
        public string ReferenceCode { get; set; }

        public string Description { get; set; }

        public string refCodeType { get; set; }

        public int sOrder { get; set; }
    }
}
