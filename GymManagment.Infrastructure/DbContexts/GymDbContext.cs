using GymManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.DbContexts
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base (options)
        {
        }
        public DbSet <Client> Clients { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
        }
    }
}

