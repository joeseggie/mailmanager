using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class NewCorrespondanceViewModel
    {
        [Required]
        public Guid MailId { get; set; }

        [Required, Display(Name = "Date")]
        public string Logged { get; set; }

        [Required]
        public string Office { get; set; }

        [Required]
        public string Details { get; set; }
    }
}