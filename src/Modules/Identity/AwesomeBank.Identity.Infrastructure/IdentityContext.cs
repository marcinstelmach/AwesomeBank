namespace AwesomeBank.Identity.Infrastructure
{
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class IdentityContext : DbContext, IUnitOfWork
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}