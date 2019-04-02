using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyTaxPortal.Models
{
    public class ImageModel
    {
        [Key]
        public int UploadID { get; set; }
        public string Filename { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        //public bool IsSelected { get; set; }
    }
}
