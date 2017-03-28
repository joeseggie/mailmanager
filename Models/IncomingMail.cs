using System;

namespace MailManager.Models
{
    public class IncomingMail
    {
        public Guid IncomingMailId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Details { get; set; }
        public DateTime IncomingDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual OutgoingMail OutgoingMail { get; set; }
    }
}