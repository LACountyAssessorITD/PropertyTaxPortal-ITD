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
        
        public int CategoryID { get; set; }

        [StringLength(1000)]
        [DisplayName("Question")]
        public string question { get; set; }

        [DisplayName("Answer")]
        public string answer { get; set; }

        public int sOrder { get; set; }

        
        [DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DisplayName("Update Date")]
        public DateTime updatedOn { get; set; }

        [Column(TypeName = "char(3)")]
        [DisplayName("Featured")]
        public string featuredCode { get; set; }

        //[NotMapped]
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public FAQ()
        {
            this.webSectionID = "POR";

        }
    }
}
