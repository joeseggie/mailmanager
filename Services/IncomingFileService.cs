using System;
using System.Collections.Generic;
using MailManager.Models;
using MailManager.Data;
using MailManager.Models.FileViewModels;
using System.Linq;

namespace MailManager.Services
{
    public class IncomingFileService : IIncomingFile
    {
        private readonly ApplicationDbContext _db;

        public IncomingFileService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<IncomingFileViewModel> IncomingFiles => _db.IncomingFiles
            .Select(i => new IncomingFileViewModel{
                IncomingFileId = i.IncomingFileId,
                FileNumber = i.FileNumber,
                IncomingDate = i.IncomingDate,
                RowVersion = i.RowVersion,
                RecordFile = i.RecordFile
            });

        public IncomingFileViewModel GetIncomingFileById(Guid incomingFileId) => IncomingFiles
            .SingleOrDefault(i => i.IncomingFileId == incomingFileId);

        public IEnumerable<IncomingFileViewModel> GetIncomingFilesByFileNumber(string fileNumber) => IncomingFiles
            .Where(i => i.FileNumber == fileNumber).ToList();

        public OperationResult NewIncomingFile(IncomingFileViewModel newIncomingFile)
        {
            _db.IncomingFiles.Add(new IncomingFile {
                FileNumber = newIncomingFile.FileNumber,
                IncomingDate = newIncomingFile.IncomingDate});
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "New incoming file added successfully" };
        }

        public OperationResult UpdateIncomingFile(IncomingFileViewModel incomingFileUpdate)
        {
            var fileForUpdate = _db.IncomingFiles.Find(incomingFileUpdate.IncomingFileId);
            
            if(fileForUpdate == null)
                return new OperationResult{ Success = false, Message = "File for update doesn't exist" };

            fileForUpdate.IncomingDate = incomingFileUpdate.IncomingDate;

            _db.Update(fileForUpdate);
            _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Incoming file updated successfully" };
        }
    }
}