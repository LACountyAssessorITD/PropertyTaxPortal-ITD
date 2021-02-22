using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FactoredBaseValue
    {
        [Key]        
        public int BaseValue { get; set; }
        public string LandValueReasonCode { get; set; }
        public string ImpValueReasonCode { get; set; }
    
    }
}