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

            builder.Property(x => x.Email).HasConversion(x => x.ToLowerInvariant(), x => x);

            builder.OwnsOne(x => x.Password)
                .Property(x => x.PasswordHash).HasColumnName("PasswordHash");
            builder.OwnsOne(x => x.Password)
                .Property(x => x.SecurityStamp).HasColumnName("SecurityStamp");

            builder.OwnsOne(x => x.IdentityDocument)
                .Property(x => x.Type).HasColumnName("DocumentType");
            builder.OwnsOne(x => x.IdentityDocument)
                .Property(x => x.Value).HasColumnName("DocumentValue");

            builder.HasMany(x => x.UserGroups)
                .WithOne()
                .HasForeignKey("UserId");

            builder.Ignore(x => x.Version);
        }
    }
}