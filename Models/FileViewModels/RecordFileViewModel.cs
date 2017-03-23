using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MailManager.Models.FileViewModels
{
    public class RecordFileViewModel
    {
        [Display(Name = "File Number")]
        [Required(ErrorMessage = "File number is required")]
        public string FileNumber { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Row version is required")]
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<IncomingFile> IncomingFiles { get; set; }
        public virtual IEnumerable<OutgoingFile> OutgoingFiles { get; set; }
    }
}