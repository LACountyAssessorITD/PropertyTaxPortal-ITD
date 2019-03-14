using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PropertyTaxPortal.Models
{
    public class NEWS
    {
        //News table fields
        [Key]
        public int NewsID { get; set; }

        [Column(TypeName = "char(3)")]
        public string WebSectionID { get; set; }

        public int UploadID { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Body { get; set; }

        [StringLength(4000)]
        public string Caption { get; set; }

        public int sOrder { get; set; }

        [Column(TypeName = "char(3)")]
        [DisplayName("Show as")]
        public string FeaturedCode { get; set; }

        [Column(TypeName = "Datetime")]
        [DisplayName("Update Date")]
        public DateTime NewsDate { get; set; }

        [Column(TypeName = "Datetime")]
        [DisplayName("Start Date")]
        public DateTime Starton { get; set; }

        [Column(TypeName = "Datetime")]
        [DisplayName("End Date")]
        public DateTime EndOn { get; set; }

        public int isGlobal { get; set; }

        [NotMapped]
        [DisplayName("Image Upload")]
        public string Image { get; set; }

        [NotMapped]
        public string Displayed {  get {
                if (this.EndOn > DateTime.Now)
                    return "Yes";
                else
                    return "no";
                
                 }  }

       

    }
}
