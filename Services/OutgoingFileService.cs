using System;
using System.Collections.Generic;
using MailManager.Models;
using MailManager.Data;
using MailManager.Models.FileViewModels;
using System.Linq;

namespace MailManager.Services
{
    public class OutgoingFileService : IOutgoingFile
    {
        private readonly ApplicationDbContext _db;

        public OutgoingFileService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<OutgoingFileViewModel> OutgoingFiles => _db.OutgoingFiles
            .Select(i => new OutgoingFileViewModel{
                OutgoingFileId = i.OutgoingFileId,
                FileNumber = i.FileNumber,
                OutgoingDate = i.OutgoingDate,
                Comment = i.Comment,
                Officer = i.Officer,
                RowVersion = i.RowVersion,
                RecordFile = i.RecordFile
            });

        public OperationResult AddOutgoingFile(OutgoingFileViewModel newOutgoingFile)
        {
            _db.OutgoingFiles.Add(new OutgoingFile {
                FileNumber = newOutgoingFile.FileNumber,
                Comment = newOutgoingFile.Comment,
                Officer = newOutgoingFile.Officer,
                OutgoingDate = newOutgoingFile.OutgoingDate});
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "New outgoing file added successfully" };
        }

        public OperationResult EditOutgoingFile(OutgoingFileViewModel outgoingFileUpdate)
        {
            var fileForUpdate = _db.OutgoingFiles.Find(outgoingFileUpdate.OutgoingFileId);
            
            if(fileForUpdate == null)
                return new OperationResult{ Success = false, Message = "File for update doesn't exist" };

            fileForUpdate.OutgoingDate = outgoingFileUpdate.OutgoingDate;

            _db.Update(fileForUpdate);
            _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Outgoing file updated successfully" };
        }

        public OutgoingFileViewModel GetOutgoingFileById(Guid outgoingFileId) => OutgoingFiles
            .SingleOrDefault(o => o.OutgoingFileId == outgoingFileId);

        public IEnumerable<OutgoingFileViewModel> GetOutgoingFilesByFileNumber(string fileNumber) => OutgoingFiles
            .Where(o => o.FileNumber == fileNumber).ToList();
    }
}