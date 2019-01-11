using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailManager.Web.Models
{
    public class Correspondance
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MailId { get; set; }

        [Required]
        public DateTime Logged { get; set; }

        [Required]
        public string Office { get; set; }

        [Required]
        public string Details { get; set; }

        [ForeignKey("MailId")]
        public virtual Mail Mail { get; set; }
    }
}