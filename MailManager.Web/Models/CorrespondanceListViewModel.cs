using System;

namespace MailManager.Web.Models
{
    public class CorrespondanceListViewModel
    {
        public Guid Id { get; set; }

        public Guid MailId { get; set; }

        public string Logged { get; set; }

        public string Office { get; set; }

        public string Details { get; set; }
    }
}