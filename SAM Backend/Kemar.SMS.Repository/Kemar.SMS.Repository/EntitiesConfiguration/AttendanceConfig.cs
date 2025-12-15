using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class AttendanceConfig : BaseEntityConfig<Attendance>
    {
        public override void Configure(EntityTypeBuilder<Attendance> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.AttendanceId);
            builder.Property(x => x.AttendanceId).ValueGeneratedOnAdd();


            builder.HasOne(a => a.Student)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(a => a.Subject)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(a => a.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(a => a.Teacher)
                   .WithMany(t => t.Attendances)
                   .HasForeignKey(a => a.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.AttendanceDate)
                   .IsRequired();

            builder.Property(x => x.IsPresent)
                   .HasDefaultValue(true)
                   .IsRequired();
        }
    }
}