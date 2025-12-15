using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class TeacherConfig : BaseEntityConfig<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.TeacherId).ValueGeneratedOnAdd();
            builder.Property(x => x.TeacherName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PhoneNo).IsRequired().HasMaxLength(10);
        }
    }
}
