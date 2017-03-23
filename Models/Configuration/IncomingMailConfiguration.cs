using MailManager.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Models.Configuration
{
    public class IncomingMailConfiguration : DbEntityConfiguration<IncomingMail>
    {
        public override void Configure(EntityTypeBuilder<IncomingMail> entity)
        {
            entity.HasKey(m => m.IncomingMailId);
            entity.Property(m => m.IncomingMailId)
                  .ValueGeneratedOnAdd();

            entity.HasOne(m => m.OfficeMail)
                  .WithMany(o => o.IncomingMails)
                  .HasForeignKey(m => m.ReferenceNumber);

            entity.Property(m => m.Details)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(150)
                  .ForSqlServerHasColumnType("varchar");
            
            entity.Property(m => m.IncomingDate)
                  .IsRequired()
                  .ForSqlServerHasColumnType("date");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();
        }
    }
}