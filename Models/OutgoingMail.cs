using System;

namespace MailManager.Models
{
    public class OutgoingMail
    {
        public Guid OutgoingMailId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Comment { get; set; }
        public string Officer { get; set; }
        public DateTime OutgoingDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual OfficeMail OfficeMail { get; set; }
    }
}