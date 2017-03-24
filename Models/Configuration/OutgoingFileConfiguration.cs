using MailManager.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Models.Configuration
{
    public class OutgoingFileConfiguration : DbEntityConfiguration<OutgoingFile>
    {
        public override void Configure(EntityTypeBuilder<OutgoingFile> entity)
        {
            entity.HasKey(m => m.OutgoingFileId);
            entity.Property(m => m.OutgoingFileId)
                  .ValueGeneratedOnAdd();

            entity.HasOne(m => m.RecordFile)
                  .WithMany(o => o.OutgoingFiles)
                  .HasForeignKey(m => m.FileNumber);

            entity.Property(m => m.Comment)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(150)
                  .ForSqlServerHasColumnType("varchar(150)");

            entity.Property(m => m.Officer)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasMaxLength(50)
                  .ForSqlServerHasColumnType("varchar(150)");
            
            entity.Property(m => m.OutgoingDate)
                  .IsRequired()
                  .ForSqlServerHasColumnType("date");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();

            entity.ToTable("OutgoingFile");
        }
    }
}