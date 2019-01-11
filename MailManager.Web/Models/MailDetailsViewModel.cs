using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class MailDetailsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Details { get; set; }

        [Required, Display(Name = "Date received")]
        public string Received { get; set; }

        public IEnumerable<CorrespondanceListViewModel> Correspondances { get; set; }

        public IEnumerable<ActionPointListViewModel> ActionPoints { get; set; }
    }
}