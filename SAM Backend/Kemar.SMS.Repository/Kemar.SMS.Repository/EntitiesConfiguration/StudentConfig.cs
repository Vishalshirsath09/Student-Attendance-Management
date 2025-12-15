using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class StudentConfig : BaseEntityConfig<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.StudentId);
            builder.Property(x => x.StudentId).ValueGeneratedOnAdd();
            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Rollno).IsRequired();
            builder.Property(x => x.Class).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Div).IsRequired().HasMaxLength(10);
            builder.Property(x => x.PhoneNo).HasMaxLength(15);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.EmailAddress).HasMaxLength(150);
            builder.HasMany(s => s.Attendances)
                   .WithOne(a => a.Student)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
