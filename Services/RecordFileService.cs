using System;
using System.Collections.Generic;
using MailManager.Models.FileViewModels;
using MailManager.Data;
using System.Linq;
using MailManager.Models;

namespace MailManager.Services
{
    public class RecordFileService : IRecordFile
    {
        private readonly ApplicationDbContext _db;

        public RecordFileService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<RecordFileViewModel> RecordFiles => _db.RecordFiles
            .Select(f => new RecordFileViewModel{
                FileNumber  = f.FileNumber,
                Subject = f.Subject,
                Stub = f.Stub,
                RowVersion = f.RowVersion,
            });

        public OperationResult AddRecordFile(RecordFileViewModel newRecordFile)
        {
            _db.RecordFiles.Add(new RecordFile{
                FileNumber = newRecordFile.FileNumber,
                Subject = newRecordFile.Subject
            });
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "Record file addedd successfully" };
        }

        public RecordFileViewModel GetRecordFileById(string fileNumber) => RecordFiles
            .SingleOrDefault(r => r.FileNumber == fileNumber);

        public OperationResult UpdateRecordFile(RecordFileViewModel recordFileUpdate)
        {
            var fileForUpdate = _db.RecordFiles.Find(recordFileUpdate.FileNumber);
            
            if(fileForUpdate == null)
                return new OperationResult{ Success = false, Message = "File for update doesn't exist" };

            fileForUpdate.Subject = recordFileUpdate.Subject;

            _db.Update(fileForUpdate);
            _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Record file updated successfully" };
        }
    }
}