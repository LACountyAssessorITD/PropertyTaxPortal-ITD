using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Have to supply a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Have to supply a password")]
        public string Password { get; set; }
    }
}
