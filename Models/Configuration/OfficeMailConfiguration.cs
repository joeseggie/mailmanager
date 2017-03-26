using MailManager.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Models.Configuration
{
    public class OfficeMailConfiguration : DbEntityConfiguration<OfficeMail>
    {
        public override void Configure(EntityTypeBuilder<OfficeMail> entity)
        {
            entity.HasKey(m => m.ReferenceNumber);

            entity.Property(m => m.ReferenceNumber)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(20)
                  .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(m => m.Subject)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(100)
                  .ForSqlServerHasColumnType("varchar(100)");
            
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

            entity.Property(m => m.Stub)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(20)
                  .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();

            entity.ToTable("OfficeMail");
        }
    }
}