using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using MailManager.Web.Models;

namespace MailManager.Web.Services
{
    public class MailService : IMailService
    {
        private readonly ApplicationDbContext _db;

        public MailService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add new mail
        /// </summary>
        /// <param name="mail">Mail to be added.</param>
        /// <returns>Mail added.</returns>
        public async Task<Mail> AddMailAsync(Mail mail)
        {
            var mailEntry = await _db.Mails.AddAsync(mail);
            await _db.SaveChangesAsync();
            return mailEntry.Entity;
        }

        /// <summary>
        /// Get all mails.
        /// </summary>
        /// <returns>Mails enumerable.</returns>
        public IQueryable<Mail> GetMail()
        {
            return _db.Mails;
        }

        /// <summary>
        /// Get mail given the Id.
        /// </summary>
        /// <param name="mailId">Mail Id.</param>
        /// <returns>Mail record details.</returns>
        public async Task<Mail> GetMailAsync(Guid mailId)
        {
            return await _db.Mails.FindAsync(mailId);
        }

        /// <summary>
        /// Update mail details.
        /// </summary>
        /// <param name="mail">Mail updates.</param>
        /// <returns>Updated mail.</returns>
        public async Task<Mail> UpdateMailAsync(Mail mail)
        {
            var mailToUpdate = await _db.Mails.FindAsync(mail.Id);
            if (mailToUpdate == null)
            {
                throw new ApplicationException("Mail to be updated was not found.");
            }

            mailToUpdate.Details = mail.Details;
            mailToUpdate.From = mail.From;
            mailToUpdate.Received = mail.Received;
            mailToUpdate.ReferenceNumber = mail.ReferenceNumber;
            mailToUpdate.Subject = mail.Subject;
            mailToUpdate.To = mail.To;

            _db.Update(mailToUpdate);
            await _db.SaveChangesAsync();

            return mailToUpdate;
        }
    }
}