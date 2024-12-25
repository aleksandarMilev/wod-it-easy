namespace WodItEasy.Infrastructure.Persistence
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Models;

    internal class WodItEasyDbContext(DbContextOptions<WodItEasyDbContext> options) : DbContext(options)
    {
        public DbSet<Athlete> Athletes { get; init; }

        public DbSet<Membership> Memberships { get; init; }

        public DbSet<Workout> Workouts { get; init; }

        public DbSet<AthleteWorkout> AthletesWorkouts { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
