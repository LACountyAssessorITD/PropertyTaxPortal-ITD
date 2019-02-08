using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PropertyTaxPortal.Models
{
    public class PublicInquiryViewModel
    {
        [Required (ErrorMessage = "Last Name is required.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string firstName { get; set; }

        public string requestForAssistance { get; set; }

    }
}
