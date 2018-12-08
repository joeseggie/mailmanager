using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailManager.Web.Models
{
    public class MailListViewModel
    {
        public Guid Id { get; set; }

        public string ReferenceNumber { get; set; }

        public string Subject { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Details { get; set; }

        public DateTime? Received { get; set; }
    }
}
