using System;
using MailManager.Models;
using System.Collections.Generic;

namespace MailManager.Services
{
    public interface IOutgoingFile
    {
         IEnumerable<OutgoingFile> OutgoingFiles { get; }
         OperationResult AddOutgoingFile(OutgoingFile newOutgoingFile);
         OperationResult EditOutgoingFile(OutgoingFile outgoingFileUpdate);
         OutgoingFile GetOutgoingFileById(Guid outgoingFileId);
         IEnumerable<OutgoingFile> GetOutgoingFilesByFileNumber(string fileNumber);
    }
}