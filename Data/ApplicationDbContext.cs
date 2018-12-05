using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MailManager.Models;
using MailManager.Extensions;
using MailManager.Models.Configuration;

namespace MailManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<IncomingMail> IncomingMails { get; set; }
        public DbSet<OutgoingMail> OutgoingMails { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.AddConfiguration(new IncomingMailConfiguration());
            builder.AddConfiguration(new OutgoingMailConfiguration());
        }
    }
}
