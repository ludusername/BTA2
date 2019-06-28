using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTA.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage ="Please enter your user name")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }
        public string LoginError { get; set; }//ovaj properti nam sluzi za ispisivanje greske u logovanju
    }
}