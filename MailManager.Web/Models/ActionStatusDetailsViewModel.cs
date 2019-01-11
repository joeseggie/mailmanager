using System;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class ActionStatusDetailsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required, Display(Name = "Action status")]
        public string Description { get; set; }
    }
}