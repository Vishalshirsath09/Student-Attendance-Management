using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class SubjectConfig : BaseEntityConfig<Subject>
    {
        public override void Configure(EntityTypeBuilder<Subject> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.SubjectId);
            builder.Property(x => x.SubjectId).ValueGeneratedOnAdd();
            builder.Property(x => x.SubjectName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SubjectCode).IsRequired().HasMaxLength(10);

            builder.HasOne(s => s.Teacher)
                   .WithMany(t => t.Subjects)
                   .HasForeignKey(s => s.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Attendances)
                   .WithOne(a => a.Subject)
                   .HasForeignKey(a => a.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
