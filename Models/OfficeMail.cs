using System.Collections.Generic;

namespace MailManager.Models
{
    /// <summary>
    /// Office mail object.
    /// </summary>
    public class OfficeMail
    {
        public string ReferenceNumber { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<IncomingMail> IncomingMails { get; set; }
        public virtual IEnumerable<OutgoingMail> OutgoingMails { get; set; }
    }
}