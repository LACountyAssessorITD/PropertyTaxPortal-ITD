using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PropertyTaxPortal.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [DisplayName("Description")]
        [Required]
        public string Descr { get; set; }

        [Column(TypeName = "char(3)")]
        [DisplayName("Type")]
        [Required]
        public string categoryType { get; set; }

        [Column(TypeName = "char(3)")]
        public string WebsectionID { get; set; }

        public int sOrder { get; set; }

        public System.Nullable<int> UploadID { get; set; }

        public Category()
        {
            this.WebsectionID = "POR";
            this.sOrder = 0;
            this.UploadID = 0;

        }
    }
 }

