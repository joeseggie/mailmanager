using System;
using MailManager.Models;
using System.Collections.Generic;
using MailManager.Models.FileViewModels;

namespace MailManager.Services
{
    public interface IIncomingFile
    {
         IEnumerable<IncomingFileViewModel> IncomingFiles { get; }
         OperationResult NewIncomingFile(IncomingFileViewModel newIncomingFile);
         OperationResult UpdateIncomingFile(IncomingFileViewModel incomingFileUpdate);
         IncomingFileViewModel GetIncomingFileById(Guid incomingFileId);
         IEnumerable<IncomingFileViewModel> GetIncomingFilesByFileNumber(string fileNumber);
    }
}