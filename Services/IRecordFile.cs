using MailManager.Models.FileViewModels;
using System.Collections.Generic;

namespace MailManager.Services
{
    public interface IRecordFile
    {
         IEnumerable<RecordFileViewModel> RecordFiles { get; }
         OperationResult AddRecordFile(RecordFileViewModel newRecordFile);
         OperationResult UpdateRecordFile(RecordFileViewModel recordFileUpdate);
         RecordFileViewModel GetRecordFileById(string fileNumber);
    }
}