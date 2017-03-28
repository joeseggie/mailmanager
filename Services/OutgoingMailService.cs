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
                IncomingMailId = m.IncomingMailId,
                Comment = m.Comment,
                Officer = m.Officer,
                OutgoingDate = m.OutgoingDate,
                RowVersion = m.RowVersion,
                IncomingMail = m.IncomingMail == null ? null : new IncomingMailViewModel{
                    IncomingMailId = m.IncomingMail.IncomingMailId,
                    ReferenceNumber = m.IncomingMail.ReferenceNumber,
                    Subject = m.IncomingMail.Subject,
                    From = m.IncomingMail.From,
                    To = m.IncomingMail.To,
                    Details = m.IncomingMail.Details,
                    IncomingDate = m.IncomingMail.IncomingDate,
                    RowVersion = m.IncomingMail.RowVersion
                }
            });

        public OperationResult EditOutgoingMail(OutgoingMailViewModel mailUpdate)
        {
            var record = _db.OutgoingMails.Find(mailUpdate.IncomingMailId);
            if(record == null)
                return new OperationResult { Success = false, Message = "Mail for update doesnot exist." };
            
            record.Comment = mailUpdate.Comment;
            record.Officer = mailUpdate.Officer;
            record.OutgoingDate = mailUpdate.OutgoingDate;

            _db.Update(record);
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "Mail updated successfully" };
        }

        public OutgoingMailViewModel GetOutgoingMailById(Guid incomingMailId) => OutgoingMails.SingleOrDefault(m => m.IncomingMailId == incomingMailId);

        public OperationResult NewOutgoingMail(OutgoingMailViewModel newMail)
        {
            _db.OutgoingMails.Add(new OutgoingMail{
                IncomingMailId = newMail.IncomingMailId,
                Comment = newMail.Comment,
                Officer = newMail.Officer,
                OutgoingDate = newMail.OutgoingDate,
            });
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "Mail added successfully" };
        }
    }
}