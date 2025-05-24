namespace WodItEasy.Workouts.Infrastructure.Persistence
{
    using System.Reflection;
    using Domain.Models.Athletes;
    using Domain.Models.Participation;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;

    internal class WorkoutDbContext(
        DbContextOptions<WorkoutDbContext> options)
        : DbContext(options)
    {
        public DbSet<Athlete> Athletes { get; set; } = default!;

        public DbSet<Workout> Workouts { get; set; } = default!;

        public DbSet<Participation> Participations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
