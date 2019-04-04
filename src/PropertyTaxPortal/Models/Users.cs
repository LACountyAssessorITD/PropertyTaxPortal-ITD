using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Models
{
    public partial class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
