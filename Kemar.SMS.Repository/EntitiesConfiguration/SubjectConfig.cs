using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class SubjectConfig : BaseEntityConfig<Subject> 
    {
        public override void Configure(EntityTypeBuilder<Subject> builder) 
        {
            base.Configure(builder);
            builder.Property(x => x.SubjectId).ValueGeneratedOnAdd();
            builder.Property(x => x.SubjectName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SubjectCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.TeacherName).IsRequired().HasMaxLength(50);
        }
    }
}
