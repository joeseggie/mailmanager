using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailManager.Extensions
{
    public abstract class DbEntityConfiguration<T> where T : class
    {
        public abstract void Configure(EntityTypeBuilder<T> entity);
    }
}