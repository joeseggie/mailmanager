using System;

namespace MailManager.Models
{
    public class OutgoingMail
    {
        public Guid IncomingMailId { get; set; }
        public string Comment { get; set; }
        public string Officer { get; set; }
        public DateTime OutgoingDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IncomingMail IncomingMail { get; set; }
    }
}