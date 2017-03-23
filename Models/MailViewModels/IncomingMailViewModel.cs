using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Models.MailViewModels
{
    public class IncomingMailViewModel
    {
        /// <summary>
        /// Incoming mail Id.
        /// </summary>
        /// <returns>Guid</returns>
        [Required(ErrorMessage = "Incoming mail Id is required")]
        public Guid IncomingMailId { get; set; }

        /// <summary>
        /// Mail reference number.
        /// </summary>
        /// <returns>string</returns>
        [Display(Name = "Reference Number")]
        [Required(ErrorMessage = "Reference number is required")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Details about the incoming mail.
        /// </summary>
        /// <returns>string</returns>
        [Required(ErrorMessage = "Details about incoming mail are required.")]
        public string Details { get; set; }

        /// <summary>
        /// Incoming mail date.
        /// </summary>
        /// <returns>DateTime</returns>
        [Display(Name = "Incoming Date")]
        [Required(ErrorMessage = "Incoming date is required")]
        public DateTime IncomingDate { get; set; }

        /// <summary>
        /// Row version.
        /// </summary>
        /// <returns>byte[]</returns>
        [Required(ErrorMessage = "Row Version is required")]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Incoming mail
        /// </summary>
        /// <returns>OfficeMail</returns>
        public virtual OfficeMailViewModel OfficeMail { get; set; }
    }
}