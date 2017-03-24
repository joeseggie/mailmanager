using System;
using MailManager.Models.FileViewModels;
using System.Collections.Generic;

namespace MailManager.Services
{
    public interface IOutgoingFile
    {
         IEnumerable<OutgoingFileViewModel> OutgoingFiles { get; }
         OperationResult AddOutgoingFile(OutgoingFileViewModel newOutgoingFile);
         OperationResult EditOutgoingFile(OutgoingFileViewModel outgoingFileUpdate);
         OutgoingFileViewModel GetOutgoingFileById(Guid outgoingFileId);
         IEnumerable<OutgoingFileViewModel> GetOutgoingFilesByFileNumber(string fileNumber);
    }
}