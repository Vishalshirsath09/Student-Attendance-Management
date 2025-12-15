using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class AttendanceConfig : BaseEntityConfig<Attendance>
    {
        public override void Configure(EntityTypeBuilder<Attendance>builder)
        {
            base.Configure(builder);
            builder.Property(x => x.AttendanceId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Student)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Subject)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(x => x.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.AttendanceDate).IsRequired();
            builder.Property(x => x.IsPresent).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}
