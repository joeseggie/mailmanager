using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class NewActionPointViewModel
    {
        [Required]
        public Guid MailId { get; set; }

        [Required, Display(Name = "Status")]
        public Guid ActionStatusId { get; set; }

        [Required]
        public string Details { get; set; }
    }
}