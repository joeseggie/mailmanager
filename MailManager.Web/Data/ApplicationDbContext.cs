using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MailManager.Web.Models;

namespace MailManager.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionPoint> ActionPoints { get; set; }
        public virtual DbSet<ActionStatus> ActionStatuses { get; set; }
        public virtual DbSet<Correspondance> Correspondances { get; set; }
        public virtual DbSet<Mail> Mails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
