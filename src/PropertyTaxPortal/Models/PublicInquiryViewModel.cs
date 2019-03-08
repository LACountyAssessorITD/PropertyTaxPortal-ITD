using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PropertyTaxPortal.Models
{
    public class PublicInquiryViewModel
    {
        [Required(ErrorMessage = "SUBJECT is required.")]
        public string subject { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }

        [Required(ErrorMessage = "LAST NAME is required.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "FIRST NAME is required.")]
        public string firstName { get; set; }

        public string businessName { get; set; }

        public string mailingAddr { get; set; }

        public string mailAddrCity { get; set; }

        public string mailAddrState { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid ZIP CODE.")]
        public string mailAddrZip { get; set; }

        [Required(ErrorMessage = "EMAIL is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid EMAIL.")]
        public string emailAddr { get; set; }

        [Required(ErrorMessage = "DAYTIME PHONE NUMBER is required.")]  
        [DataType(DataType.PhoneNumber)]  
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid DAYTIME PHONE NUMBER.")]
        public string dayTimeTelNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid FAX NUMBER.")]
        public string faxNumber { get; set; }


        public string propertyAddr { get; set; }

        public string propertyAddrCity { get; set; }

        public string propertyAddrState { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid ZIP CODE.")]
        public string propertyAddrZip { get; set; }

        [Range(0, 999999999999, ErrorMessage = "Invalid AIN.")]
        public string AIN { get; set; }


        public string companyNumber { get; set; }

        public string routingIndex { get; set; }

        [Required(ErrorMessage = "COMMENT is required.")]
        public string comment { get; set; }

    }
}
