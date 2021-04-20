using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class ActionPointDetailsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid? MailId { get; set; }

        [Required]
        public Guid? ActionStatusId { get; set; }

        [Required]
        public string Details { get; set; }
    }
}