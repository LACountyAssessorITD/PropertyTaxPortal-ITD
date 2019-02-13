using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PropertyTaxPortal.Models
{
    public class FAQ
    {
        [Key]
        public int FAQID { get; set; }

        [Column(TypeName = "char(3)")]
        public string webSectionID { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [StringLength(1000)]
        [DisplayName("Question")]
        public string question { get; set; }

        [DisplayName("Answer")]
        public string answer { get; set; }

        public int sOrder { get; set; }

        [Column(TypeName = "Datetime")]
        [DisplayName("Update Date")]
        public DateTime updatedOn { get; set; }

        [Column(TypeName = "char(3)")]
        [DisplayName("Featured")]
        public string featuredCode { get; set; }

        public FAQ()
        {
            this.webSectionID = "POR";

        }
    }
}
