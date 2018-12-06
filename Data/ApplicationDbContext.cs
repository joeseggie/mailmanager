using System;
using System.Collections.Generic;
using System.Text;
using MailManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MailManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
