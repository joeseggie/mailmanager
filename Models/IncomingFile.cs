using System;

namespace MailManager.Models
{
    public class IncomingFile
    {
        public Guid IncomingFileId { get; set; }
        public string FileNumber { get; set; }
        public DateTime IncomingDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual RecordFile RecordFile { get; set; }
    }
}