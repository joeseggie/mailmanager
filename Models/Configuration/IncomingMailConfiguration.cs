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

            entity.HasOne(m => m.OutgoingMail)
                  .WithOne(o => o.IncomingMail);

            entity.Property(m => m.ReferenceNumber)
                  .IsUnicode(false)
                  .HasMaxLength(20)
                  .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(m => m.Subject)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(100)
                  .ForSqlServerHasColumnType("varchar(1500)");
            
            entity.Property(m => m.From)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(50)
                  .ForSqlServerHasColumnType("varchar(50)");
            
            entity.Property(m => m.To)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(50)
                  .ForSqlServerHasColumnType("varchar(50)");

            entity.Property(m => m.Details)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(150)
                  .ForSqlServerHasColumnType("varchar(1500)");
            
            entity.Property(m => m.IncomingDate)
                  .IsRequired()
                  .ForSqlServerHasColumnType("date");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();

            entity.ToTable("IncomingMail");
        }
    }
}