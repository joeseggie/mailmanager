using MailManager.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Models.Configuration
{
    public class RecordFileConfiguration : DbEntityConfiguration<RecordFile>
    {
        public override void Configure(EntityTypeBuilder<RecordFile> entity)
        {
            entity.HasKey(m => m.FileNumber);

            entity.Property(m => m.FileNumber)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(20)
                  .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(m => m.Subject)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(150)
                  .ForSqlServerHasColumnType("varchar(150)");

            entity.Property(m => m.Stub)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(20)
                  .ForSqlServerHasColumnType("varchar(20)");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();

            entity.ToTable("RecordFile");
        }
    }
}