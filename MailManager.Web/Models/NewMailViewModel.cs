using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class NewMailViewModel
    {
        [Required, Display(Name = "Reference No.")]
        public string ReferenceNumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Details { get; set; }

        [Required, Display(Name = "Date received")]
        public string Received { get; set; }
    }
}