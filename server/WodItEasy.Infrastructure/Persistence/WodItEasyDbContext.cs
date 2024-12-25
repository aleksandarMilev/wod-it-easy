namespace WodItEasy.Infrastructure.Persistence
{
    using System.Reflection;
    using Domain.Models.Athletes;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;

    internal class WodItEasyDbContext(DbContextOptions<WodItEasyDbContext> options) : DbContext(options)
    {
        public DbSet<Athlete> Athletes { get; init; }

        public DbSet<Membership> Memberships { get; init; }

        public DbSet<Workout> Workouts { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
