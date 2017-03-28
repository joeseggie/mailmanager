using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.MailViewModels
{
    public class OutgoingMailViewModel
    {
        [Required(ErrorMessage = "Incoming mail Id is required")]
        public Guid IncomingMailId { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Officer is required")]
        public string Officer { get; set; }

        [Display(Name = "Outgoing Date")]
        public DateTime OutgoingDate { get; set; }
        
        [Required(ErrorMessage = "Row version is required")]
        public byte[] RowVersion { get; set; }

        public virtual IncomingMailViewModel IncomingMail { get; set; }
    }
}