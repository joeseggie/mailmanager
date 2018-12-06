using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using MailManager.Web.Models;

namespace MailManager.Web.Services
{
    public class ActionPointService : IActionPointService
    {
        private readonly ApplicationDbContext _db;

        public ActionPointService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add action point.
        /// </summary>
        /// <param name="actionPoint">Action point to add.</param>
        /// <returns>Action point added.</returns>
        public async Task<ActionPoint> AddActionPointAsync(ActionPoint actionPoint)
        {
            var actionPointEntry = await _db.ActionPoints.AddAsync(actionPoint);
            return actionPointEntry.Entity;
        }

        /// <summary>
        /// Get action point given the Id.
        /// </summary>
        /// <param name="actionPointId">Action point Id.</param>
        /// <returns>Action point</returns>
        public async Task<ActionPoint> GetActionPointAsync(Guid actionPointId)
        {
            return await _db.ActionPoints.FindAsync(actionPointId);
        }

        /// <summary>
        /// Enumerate action points.
        /// </summary>
        /// <returns>Action points enumerable.</returns>
        public IQueryable<ActionPoint> GetActionPoints()
        {
            return _db.ActionPoints;
        }

        /// <summary>
        /// Update action point.
        /// </summary>
        /// <param name="actionPoint">Action point updates.</param>
        /// <returns>Updated action point.</returns>
        public async Task<ActionPoint> UpdateActionPointAsync(ActionPoint actionPoint)
        {
            var actionPointToUpdate = await _db.ActionPoints.FindAsync(actionPoint.Id);
            if (actionPointToUpdate == null)
            {
                throw new ApplicationException("Action point to update was not found.");
            }

            actionPointToUpdate.ActionStatusId = actionPoint.ActionStatusId;
            actionPointToUpdate.Details = actionPoint.Details;
            actionPointToUpdate.MailId = actionPoint.MailId;

            _db.Update(actionPointToUpdate);
            await _db.SaveChangesAsync();

            return actionPointToUpdate;
        }
    }
}