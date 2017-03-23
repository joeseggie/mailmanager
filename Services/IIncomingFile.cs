using System;
using MailManager.Models;
using System.Collections.Generic;

namespace MailManager.Services
{
    public interface IIncomingFile
    {
         IEnumerable<IncomingFile> IncomingFiles { get; }
         OperationResult NewIncomingFile(IncomingFile newIncomingFile);
         OperationResult UpdateIncomingFile(IncomingFile incomingFileUpdate);
         IncomingFile GetIncomingFileById(Guid incomingFileId);
         IEnumerable<IncomingFile> GetIncomingFilesByFileNumber(string fileNumber);
    }
}