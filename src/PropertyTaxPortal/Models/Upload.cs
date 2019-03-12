using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyTaxPortal.Models
{
    public class Upload
    {
        [Key]
        public int UploadID { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }

        public byte ImageData { get; set; }

        [StringLength(30)]
        public string ContentType { get; set; }

        [Column(TypeName = "char(3)")]
        public string UploadType { get; set; }

        [StringLength(3)]
        public string imageType { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime UpdatedOn { get; set; }

        public int ImageSize { get; set; }
    }
}
