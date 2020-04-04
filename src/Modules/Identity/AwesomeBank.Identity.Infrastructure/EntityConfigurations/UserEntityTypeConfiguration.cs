namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        private const string UserTableName = "Users";
        private readonly string _schemaName;

        public UserEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(UserTableName, _schemaName);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));

            builder.OwnsOne(x => x.Password)
                .Property(x => x.PasswordHash).HasColumnName("PasswordHash");
            builder.OwnsOne(x => x.Password)
                .Property(x => x.SecurityStamp).HasColumnName("SecurityStamp");

            builder.Property<int>("RoleId");
            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey("RoleId");

            builder.HasMany(x => x.ApplicationUserGroups)
                .WithOne()
                .HasForeignKey("UserId");

            builder.Ignore(x => x.Version);
            builder.Ignore(x => x.Claims);
        }
    }
}