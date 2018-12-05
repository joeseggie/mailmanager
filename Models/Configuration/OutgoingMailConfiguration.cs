using MailManager.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Models.Configuration
{
    public class OutgoingMailConfiguration : DbEntityConfiguration<OutgoingMail>
    {
        public override void Configure(EntityTypeBuilder<OutgoingMail> entity)
        {
            entity.HasKey(m => m.IncomingMailId);

            entity.Property(m => m.Comment)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(150)
                  .ForSqlServerHasColumnType("varchar(1500)");

            entity.Property(m => m.Officer)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(50)
                  .ForSqlServerHasColumnType("varchar(500)");
            
            entity.Property(m => m.OutgoingDate)
                  .IsRequired()
                  .ForSqlServerHasColumnType("date");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();

            entity.ToTable("OutgoingMail");
        }
    }
}