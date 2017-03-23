using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.MailViewModels
{
    public class OfficeMailViewModel
    {
        /// <summary>
        /// Mail reference number.
        /// </summary>
        /// <returns>string</returns>
        [Display(Name = "Reference Number")]
        [Required(ErrorMessage = "Reference number is required")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Mail subject.
        /// </summary>
        /// <returns>string</returns>
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        /// <summary>
        /// Sender of mail.
        /// </summary>
        /// <returns>string</returns>
        [Display(Name = "Sender")]
        [Required(ErrorMessage = "Sender is required")]
        public string From { get; set; }

        /// <summary>
        /// Recipient of mail.
        /// </summary>
        /// <returns>string</returns>
        [Display(Name = "Recipeint")]
        [Required(ErrorMessage = "Recipeint is required")]
        public string To { get; set; }

        /// <summary>
        /// Record version.
        /// </summary>
        /// <returns>byte[]</returns>
        [Required(ErrorMessage = "Row version is required")]
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<IncomingMailViewModel> IncomingMails { get; set; }
        public virtual IEnumerable<OutgoingMailViewModel> OutgoingMails { get; set; }
    }
}