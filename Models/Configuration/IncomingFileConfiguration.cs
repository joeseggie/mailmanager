using MailManager.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Models.Configuration
{
    public class IncomingFileConfiguration : DbEntityConfiguration<IncomingFile>
    {
        public override void Configure(EntityTypeBuilder<IncomingFile> entity)
        {
            entity.HasKey(m => m.IncomingFileId);
            entity.Property(m => m.IncomingFileId)
                  .ValueGeneratedOnAdd();

            entity.HasOne(m => m.RecordFile)
                  .WithMany(o => o.IncomingFiles)
                  .HasForeignKey(m => m.FileNumber);
            
            entity.Property(m => m.IncomingDate)
                  .IsRequired()
                  .ForSqlServerHasColumnType("date");

            entity.Property(m => m.RowVersion)
                  .IsRequired()
                  .IsRowVersion();

            entity.ToTable("IncomingFile");
        }
    }
}