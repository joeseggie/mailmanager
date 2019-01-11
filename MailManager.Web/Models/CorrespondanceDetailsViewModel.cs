using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class CorrespondanceDetailsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid? MailId { get; set; }

        [Required, Display(Name = "Date logged")]
        public string Logged { get; set; }

        [Required]
        public string Details { get; set; }
        public string Office { get; set; }
    }
}