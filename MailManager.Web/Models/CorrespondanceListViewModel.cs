using System;

namespace MailManager.Web.Models
{
    public class CorrespondanceListViewModel
    {
        public Guid Id { get; set; }

        public Guid MailId { get; set; }

        public DateTime Logged { get; set; }

        public string Office { get; set; }

        public string Details { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public DateTime Received { get; set; }
    }
}