using System;
using System.Collections.Generic;

namespace PropertyTaxPortal.Models
{
    public partial class News
    {
  
        public int NewsId { get; set; }
        public int Active { get; set; }
        public int SOrder { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Caption { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
        public int? UploadId { get; set; }


    }
}
