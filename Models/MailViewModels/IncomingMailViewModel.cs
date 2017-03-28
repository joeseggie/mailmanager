using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.MailViewModels
{
    public class IncomingMailViewModel
    {
        [Required(ErrorMessage = "Incoming mail Id is required")]
        public Guid IncomingMailId { get; set; }

        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mail sender is required")]
        public string From { get; set; }

        [Required(ErrorMessage = "Mail recipient is required")]
        public string To { get; set; }

        [Required(ErrorMessage = "Details are required")]
        public string Details { get; set; }

        [Display(Name = "Incoming Date")]
        [Required(ErrorMessage = "Incoming date are required")]
        public DateTime IncomingDate { get; set; }

        [Required(ErrorMessage = "Row version required")]
        public byte[] RowVersion { get; set; }

        public virtual OutgoingMailViewModel OutgoingMail { get; set; }
    }
}