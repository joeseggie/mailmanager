using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailManager.Web.Models
{
    public class Mail
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Reference number")]
        public string ReferenceNumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        public string Details { get; set; }

        [Required]
        public DateTime Received { get; set; }
    }
}