namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClaimsEntityTypeConfiguration : IEntityTypeConfiguration<Claim>
    {
        private const string ClaimsTableName = "Claims";
        private readonly string _schemaName;

        public ClaimsEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable(ClaimsTableName, _schemaName);
            builder.HasKey(x => x.Id);
        }
    }
}