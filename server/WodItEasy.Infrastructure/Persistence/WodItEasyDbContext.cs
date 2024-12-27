namespace WodItEasy.Infrastructure.Persistence
{
    using System.Reflection;
    using Domain.Models.Athletes;
    using Domain.Models.Workouts;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Infrastructure.Identity;

    internal class WodItEasyDbContext : IdentityDbContext<User>
    {
        public WodItEasyDbContext(DbContextOptions<WodItEasyDbContext> options)
            : base(options)
        { }

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
