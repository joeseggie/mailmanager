using System.Collections.Generic;

namespace MailManager.Models
{
    public class RecordFile
    {
        public string FileNumber { get; set; }
        public string Subject { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual IEnumerable<IncomingFile> IncomingFiles { get; set; }
        public virtual IEnumerable<OutgoingFile> OutgoingFiles { get; set; }
    }
}