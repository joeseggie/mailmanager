using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailManager.Models
{
    public class CorrespondanceAction
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Details { get; set; }

        public Guid? MailId { get; set; }

        public Guid? ActionStatusId { get; set; }

        [ForeignKey("MailId")]
        public virtual Mail Mail { get; set; }

        [ForeignKey("ActionStatusId")]
        public virtual ActionStatus ActionStatus { get; set; }     
    }
}