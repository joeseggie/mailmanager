using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.FileViewModels
{
    public class IncomingFileViewModel
    {
        [Required(ErrorMessage = "Incoming file Id is required")]
        public Guid IncomingFileId { get; set; }

        [Display(Name = "File Number")]
        [Required(ErrorMessage = "File number is required")]
        public string FileNumber { get; set; }

        [Display(Name = "Incoming Date")]
        [Required(ErrorMessage = "Incoming date is required")]
        public DateTime IncomingDate { get; set; }

        [Required(ErrorMessage = "Row version is required")]
        public byte[] RowVersion { get; set; }

        public virtual RecordFile RecordFile { get; set; }
    }
}