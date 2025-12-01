
using Kemar.SMS.Repository.Entity.BaseEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100);

            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.Property(x => x.UpdatedAt).IsRequired();

            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
