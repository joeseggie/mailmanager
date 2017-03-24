using System;
using System.Collections.Generic;
using MailManager.Models.MailViewModels;
using MailManager.Models;
using MailManager.Data;
using System.Linq;

namespace MailManager.Services
{
    public class OutgoingMailService : IOutgoingMail
    {
        private readonly ApplicationDbContext _db;

        public OutgoingMailService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<OutgoingMailViewModel> OutgoingMails => _db.OutgoingMails
            .Select(m => new OutgoingMailViewModel{
                OutgoingMailId = m.OutgoingMailId,
                ReferenceNumber = m.ReferenceNumber,
                Comment = m.Comment,
                Officer = m.Officer,
                OutgoingDate = m.OutgoingDate,
                RowVersion = m.RowVersion
            });

        public OperationResult EditOutgoingMail(OutgoingMailViewModel outgoingMailUpdate)
        {
            var mailForUpdate = _db.OutgoingMails.Find(outgoingMailUpdate.OutgoingMailId);
            
            if(mailForUpdate == null)
                return new OperationResult{ Success = false, Message = "Mail for update doesn't exist" };

            mailForUpdate.OutgoingDate = outgoingMailUpdate.OutgoingDate;
            mailForUpdate.Comment = outgoingMailUpdate.Comment;
            mailForUpdate.Officer = outgoingMailUpdate.Officer;

            _db.Update(mailForUpdate);
            _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Outgoing mail updated successfully" };
        }

        public OutgoingMailViewModel GetOutgoingMailById(Guid outgoingMailId) => OutgoingMails
            .SingleOrDefault(m => m.OutgoingMailId == outgoingMailId);

        public IEnumerable<OutgoingMailViewModel> GetOutgoingMailsByReferenceNumber(string referenceNumber) => OutgoingMails
            .Where(m => m.ReferenceNumber == referenceNumber).ToList();

        public OperationResult NewOutgoingMail(OutgoingMailViewModel newOutgoingMail)
        {
            _db.OutgoingMails.Add(new OutgoingMail{
                ReferenceNumber = newOutgoingMail.ReferenceNumber,
                Comment = newOutgoingMail.Comment,
                Officer = newOutgoingMail.Officer,
                OutgoingDate = newOutgoingMail.OutgoingDate
            });
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "New outgoing mail added successfully" };
        }
    }
}