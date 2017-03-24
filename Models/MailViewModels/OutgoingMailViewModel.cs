using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.MailViewModels
{
    public class OutgoingMailViewModel
    {
        [Required(ErrorMessage = "Outgoing mail Id is required")]
        public Guid OutgoingMailId { get; set; }

        [Display(Name = "Reference Number")]
        [Required(ErrorMessage = "Reference number is required")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Officer is required")]
        public string Officer { get; set; }

        [Display(Name = "Outgoing Date")]
        [Required(ErrorMessage = "Outgoing date is required")]
        public DateTime OutgoingDate { get; set; }

        [Required(ErrorMessage = "Row version is required")]
        public byte[] RowVersion { get; set; }
    }
}