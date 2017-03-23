using MailManager.Models;
using System.Collections.Generic;

namespace MailManager.Services
{
    public interface IRecordFile
    {
         IEnumerable<RecordFile> RecordFiles { get; }
         OperationResult AddRecordFile(RecordFile newRecordFile);
         OperationResult UpdateRecordFile(RecordFile recordFileUpdate);
         RecordFile GetRecordFileById(string fileNumber);
    }
}