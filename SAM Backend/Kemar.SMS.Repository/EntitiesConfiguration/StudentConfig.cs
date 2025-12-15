using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class StudentConfig : BaseEntityConfig<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.StudentId).ValueGeneratedOnAdd();
            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Rollno).IsRequired();
            builder.Property(x => x.Class).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Div).IsRequired().HasMaxLength(5);
            builder.Property(x => x.PhoneNo).HasMaxLength(15);
            builder.Property(x => x.Address).HasMaxLength(250);
            builder.Property(x => x.EmailAddress).HasMaxLength(100);
        }
    }
}
