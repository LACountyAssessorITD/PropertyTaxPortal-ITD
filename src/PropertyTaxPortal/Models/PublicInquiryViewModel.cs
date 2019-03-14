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
        
        public string subjectText { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string subjectValue { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string firstName { get; set; }

        public string businessName { get; set; }

        public string mailingAddr { get; set; }

        public string mailAddrCity { get; set; }

        public string mailAddrState { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [RegularExpression(@"^[\d-]+$", ErrorMessage = "Invalid zip code.")]
        public string mailAddrZip { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid EMAIL.")]
        public string emailAddr { get; set; }

        [Required(ErrorMessage = "Daytime phone number is required.")]  
        [DataType(DataType.PhoneNumber)]  
        [RegularExpression(@"^[\d- ()]+$", ErrorMessage = "Invalid daytime phone number.")]
        public string dayTimeTelNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[\d- ()]+$", ErrorMessage = "Invalid FAX NUMBER.")]
        public string faxNumber { get; set; }
       
        //////////////////////////////////////////////////////////////////////////
        public string propertyAddr { get; set; }

        public string propertyAddrCity { get; set; }

        public string propertyAddrState { get; set; }

        [RegularExpression(@"^[\d-]+$", ErrorMessage = "Invalid zip code.")]
        public string propertyAddrZip { get; set; }

        [RegularExpression(@"^[0-9]{10,}$", ErrorMessage = "Invalid AIN.")]
        public string AIN { get; set; }

        //////////////////////////////////////////////////////////////////////////
        public string companyNumber { get; set; }

        public string routingIndex { get; set; }

        //////////////////////////////////////////////////////////////////////////
        [Required(ErrorMessage = "Comments is required.")]
        public string comment { get; set; }

        //////////////////////////////////////////////////////////////////////////
        public string responsibleDepartment { get; set; }

        public string addressPhoneWebsite { get; set; }
    }
}
