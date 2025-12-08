using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class TeacherConfig : BaseEntityConfig<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.TeacherId);
            builder.Property(x => x.TeacherId).ValueGeneratedOnAdd();
            builder.Property(x => x.TeacherName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.PhoneNo).HasMaxLength(15);
            builder.Property(x => x.EmailAddress).HasMaxLength(150);
            builder.HasMany(t => t.Subjects)
                   .WithOne(s => s.Teacher)
                   .HasForeignKey(s => s.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
