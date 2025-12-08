using Kemar.SMS.Repository.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.SMS.Repository.EntitiesConfiguration
{
    internal class UserConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).ValueGeneratedOnAdd();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            //builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Role).IsRequired().HasMaxLength(20);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        }
    }
}
