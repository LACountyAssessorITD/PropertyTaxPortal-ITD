using System.ComponentModel.DataAnnotations;

namespace PropertyTaxPortal.Models
{
    public class Address
    {
        [Key]        
        public string SitusAddress { get; set; }
        public string AIN { get; set; }
    }
}