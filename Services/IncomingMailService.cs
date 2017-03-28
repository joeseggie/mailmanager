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
                Subject = m.Subject,
                From = m.From,
                To = m.To,
                Details = m.Details,
                IncomingDate = m.IncomingDate,
                RowVersion = m.RowVersion,
                OutgoingMail = m.OutgoingMail == null ? null : new OutgoingMailViewModel{
                    IncomingMailId = m.OutgoingMail.IncomingMailId,
                    Comment = m.OutgoingMail.Comment,
                    Officer = m.OutgoingMail.Officer,
                    OutgoingDate = m.OutgoingMail.OutgoingDate,
                    RowVersion = m.OutgoingMail.RowVersion
                }
            });

        public OperationResult EditIncomingMail(IncomingMailViewModel mailUpdate)
        {
            var record = _db.IncomingMails.Find(mailUpdate.IncomingMailId);
            if(record == null)
                return new OperationResult { Success = false, Message = "Mail for update doesnot exist." };
            
            record.ReferenceNumber = mailUpdate.ReferenceNumber;
            record.Subject = mailUpdate.Subject;
            record.From = mailUpdate.From;
            record.To = mailUpdate.To;
            record.Details = mailUpdate.Details;
            record.IncomingDate = mailUpdate.IncomingDate;

            _db.Update(record);
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "Mail updated successfully" };
        }

        public IncomingMailViewModel GetIncomingMailById(Guid incomingMailId) => IncomingMails.SingleOrDefault(m => m.IncomingMailId == incomingMailId);

        public OperationResult NewIncomingMail(IncomingMailViewModel newMail)
        {
            _db.IncomingMails.Add(new IncomingMail{
                ReferenceNumber = newMail.ReferenceNumber,
                Subject = newMail.Subject,
                From = newMail.From,
                To = newMail.To,
                Details = newMail.Details,
                IncomingDate = newMail.IncomingDate
            });
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "Mail added successfully" };
        }
    }
}