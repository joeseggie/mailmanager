using System;

namespace MailManager.Models
{
    public class OutgoingFile
    {
        public Guid OutgoingFileId { get; set; }
        public string FileNumber { get; set; }
        public string Comment { get; set; }
        public string Officer { get; set; }
        public DateTime OutgoingDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual RecordFile RecordFile { get; set; }
    }
}