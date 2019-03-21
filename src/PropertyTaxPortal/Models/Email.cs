using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public class Email
    { 
        public string host { get; set; }
        public int port { get; set; }
        public bool enableSsl { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public bool isBodyHtml { get; set; }
        public string mailTo { get; set; }
        public string bcc { get; set; }
    }
}
