using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailManager.Models
{
    public class ActionStatus
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, Display(Name = "Description")]
        public string Description { get; set; }
    }
}