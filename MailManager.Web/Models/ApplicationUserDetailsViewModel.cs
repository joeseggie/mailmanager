using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailManager.Web.Models
{
    public class ApplicationUserDetailsViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}