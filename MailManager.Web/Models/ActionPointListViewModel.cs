using System;

namespace MailManager.Web.Models
{
    public class ActionPointListViewModel
    {
        public Guid Id { get; set; }

        public string Details { get; set; }

        public string ActionStatus { get; set; }

        public Guid? ActionStatusId { get; set; }
    }
}