using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using MailManager.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MailManager.Web.Services
{
    public class CorrespondanceService : ICorrespondanceService
    {
        private readonly ApplicationDbContext _db;

        public CorrespondanceService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add correspondance.
        /// </summary>
        /// <param name="correspondance">Correspondance to add.</param>
        /// <returns>Correspondance added.</returns>
        public async Task<Correspondance> AddCorreespondanceAsync(Correspondance correspondance)
        {
            var correspondanceEntry = await _db.Correspondances.AddAsync(correspondance);
            await _db.SaveChangesAsync();
            return correspondanceEntry.Entity;
        }

        /// <summary>
        /// Get correspondance given the correspondance Id.
        /// </summary>
        /// <param name="correspondanceId">Correspondance Id.</param>
        /// <returns>Correspondance.</returns>
        public async Task<Correspondance> GetCorrespondanceAsync(Guid correspondanceId)
        {
            return await _db.Correspondances.FindAsync(correspondanceId);
        }

        /// <summary>
        /// Get all correspondances.
        /// </summary>
        /// <returns>Correspondances enumerable.</returns>
        public IQueryable<Correspondance> GetCorrespondances()
        {
            return _db.Correspondances.Include(correspondance => correspondance.Mail);
        }

        /// <summary>
        /// Update correspondance.
        /// </summary>
        /// <param name="correspondance">Correspondance updates.</param>
        /// <returns>Updated correspondance.</returns>
        public async Task<Correspondance> UpdateCorrespondanceAsync(Correspondance correspondance)
        {
            var correspondanceToUpdate = await _db.Correspondances.FindAsync(correspondance.Id);
            if (correspondanceToUpdate == null)
            {
                throw new ApplicationException("Correspondance for update was not found.");
            }

            correspondanceToUpdate.Logged = correspondance.Logged;
            correspondanceToUpdate.MailId = correspondance.MailId;
            correspondanceToUpdate.Office = correspondance.Office;
            correspondanceToUpdate.Details = correspondance.Details;

            _db.Update(correspondanceToUpdate);
            await _db.SaveChangesAsync();

            return correspondanceToUpdate;
        }
    }
}