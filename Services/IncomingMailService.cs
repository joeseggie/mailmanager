using System;
using System.Collections.Generic;
using MailManager.Models;
using MailManager.Models.MailViewModels;
using MailManager.Data;
using System.Linq;

namespace MailManager.Services
{
    public class IncomingMailService : IIncomingMail
    {
        private readonly ApplicationDbContext _db;

        public IncomingMailService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<IncomingMailViewModel> IncomingMails => _db.IncomingMails
            .Select(m => new IncomingMailViewModel{
                IncomingMailId = m.IncomingMailId,
                ReferenceNumber = m.ReferenceNumber,
                Details = m.Details,
                IncomingDate = m.IncomingDate,
                RowVersion = m.RowVersion
            });

        public OperationResult EditIncomingMail(IncomingMailViewModel mailUpdate)
        {
            var mailForUpdate = _db.IncomingMails.Find(mailUpdate.IncomingMailId);
            
            if(mailForUpdate == null)
                return new OperationResult{ Success = false, Message = "Mail for update doesn't exist" };

            mailForUpdate.Details = mailUpdate.Details;
            mailForUpdate.IncomingDate = mailUpdate.IncomingDate;

            _db.Update(mailForUpdate);
            _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Outgoing file updated successfully" };
        }

        public IncomingMailViewModel GetIncomingMailById(Guid incomingMailId) => IncomingMails
            .SingleOrDefault(m => m.IncomingMailId == incomingMailId);

        public IEnumerable<IncomingMailViewModel> GetIncomingMailsByReferenceNumber(string referenceNumber) => IncomingMails
            .Where(m => m.ReferenceNumber == referenceNumber).ToList();

        public OperationResult NewIncomingMail(IncomingMailViewModel newMail)
        {
            _db.IncomingMails.Add(new IncomingMail{
                ReferenceNumber = newMail.ReferenceNumber,
                Details = newMail.Details,
                IncomingDate = newMail.IncomingDate
            });
            _db.SaveChanges();

            return new OperationResult { Success = true, Message = "Incoming mail added successfully" };
        }
    }
}