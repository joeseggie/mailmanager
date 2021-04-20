using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using MailManager.Web.Models;

namespace MailManager.Web.Services
{
    public class ActionStatusService : IActionStatusService
    {
        private readonly ApplicationDbContext _db;

        public ActionStatusService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add new action status.
        /// </summary>
        /// <param name="actionStatus">Action status to add.</param>
        /// <returns>Action status added.</returns>
        public async Task<ActionStatus> AddActionStatusAsync(ActionStatus actionStatus)
        {
            var actionStatusEntry = await _db.ActionStatuses.AddAsync(actionStatus);
            await _db.SaveChangesAsync();
            return actionStatusEntry.Entity;
        }

        /// <summary>
        /// Get action status given the action status Id.
        /// </summary>
        /// <param name="actionStatusId">Action status Id.</param>
        /// <returns>Action status</returns>
        public async Task<ActionStatus> GetActionStatusAsync(Guid actionStatusId)
        {
            return await _db.ActionStatuses.FindAsync(actionStatusId);
        }

        /// <summary>
        /// Get all the action statuses.
        /// </summary>
        /// <returns>Action status enumerable.</returns>
        public IQueryable<ActionStatus> GetActionStatuses()
        {
            return _db.ActionStatuses;
        }

        /// <summary>
        /// Update action status.
        /// </summary>
        /// <param name="actionStatus">Action status update.</param>
        /// <returns>Updated action status.</returns>
        public async Task<ActionStatus> UpdateActionStatusAsync(ActionStatus actionStatus)
        {
            var actionStatusToUpdate = await _db.ActionStatuses.FindAsync(actionStatus.Id);
            if (actionStatusToUpdate == null)
            {
                throw new ApplicationException("Action status for update was not found.");
            }

            actionStatusToUpdate.Description = actionStatus.Description;

            _db.Update(actionStatusToUpdate);
            await _db.SaveChangesAsync();

            return actionStatusToUpdate;
        }
    }
}