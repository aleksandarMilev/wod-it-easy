namespace WodItEasy.Infrastructure.Persistence
{
    using System.Collections.Generic;
    using System.Reflection;
    using Domain.Common;
    using Domain.Models.Athletes;
    using Domain.Models.Workouts;
    using Identity;
    using Interceptors;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    internal class WodItEasyDbContext : IdentityDbContext<User>
    {
        private readonly PublishDomainEventInterceptor eventInterceptor;

        public WodItEasyDbContext(
            DbContextOptions<WodItEasyDbContext> options,
            PublishDomainEventInterceptor eventInterceptor)
            : base(options)
                => this.eventInterceptor = eventInterceptor;

        public DbSet<Athlete> Athletes { get; init; }

        public DbSet<Membership> Memberships { get; init; }

        public DbSet<Workout> Workouts { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this.eventInterceptor);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
