namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationGroupEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationGroup>
    {
        private const string ApplicationGroupTableName = "ApplicationGroups";
        private readonly string _schemaName;

        public ApplicationGroupEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<ApplicationGroup> builder)
        {
            builder.ToTable(ApplicationGroupTableName, _schemaName);

            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ApplicationGroupClaims)
                .WithOne();
        }
    }
}