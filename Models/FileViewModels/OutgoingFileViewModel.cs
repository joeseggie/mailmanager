using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.FileViewModels
{
    public class OutgoingFileViewModel
    {
        [Required(ErrorMessage = "Outgoing file Id is required")]
        public Guid OutgoingFileId { get; set; }

        [Display(Name = "File Number")]
        [Required(ErrorMessage = "File number is required")]
        public string FileNumber { get; set; }

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