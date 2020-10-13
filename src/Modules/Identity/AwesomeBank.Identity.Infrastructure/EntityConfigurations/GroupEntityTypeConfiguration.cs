namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string ApplicationGroupTableName = "Groups";
        private readonly string _schemaName;

        public GroupEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(ApplicationGroupTableName, _schemaName);

            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.GroupClaims)
                .WithOne();
        }
    }
}