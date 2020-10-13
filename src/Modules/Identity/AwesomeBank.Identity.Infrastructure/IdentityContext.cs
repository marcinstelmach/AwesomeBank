namespace AwesomeBank.Identity.Infrastructure
{
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Infrastructure.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class IdentityContext : DbContext, IUnitOfWork
    {
        private const string IdentitySchemaName = "Identity";

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfiguration(new UserEntityTypeConfiguration(IdentitySchemaName))
                .ApplyConfiguration(new UserGroupEntityTypeConfiguration(IdentitySchemaName))
                .ApplyConfiguration(new GroupEntityTypeConfiguration(IdentitySchemaName))
                .ApplyConfiguration(new GroupClaimEntityTypeConfiguration(IdentitySchemaName));
        }
    }
}