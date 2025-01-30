namespace WodItEasy.Infrastructure.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts;
    using Domain.Common;
    using Domain.Models.Athletes;
    using Domain.Models.Participation;
    using Domain.Models.Workouts;
    using Identity;
    using Interceptors;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    internal class WodItEasyDbContext : IdentityDbContext<User>
    {
        private readonly PublishDomainEventInterceptor eventInterceptor;
        private readonly ICurrentUserService userService;

        public WodItEasyDbContext(
            DbContextOptions<WodItEasyDbContext> options,
            PublishDomainEventInterceptor eventInterceptor,
            ICurrentUserService userService)
                : base(options)
        {
            this.eventInterceptor = eventInterceptor;
            this.userService = userService;
        }

        public DbSet<Athlete> Athletes { get; init; }

        public DbSet<Workout> Workouts { get; init; }

        public DbSet<Participation> Participations { get; init; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfo();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            FilterDeletedModels(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this.eventInterceptor);

            base.OnConfiguring(optionsBuilder);
        }

        private void ApplyAuditInfo()
            => this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(e =>
                {
                    var utcNow = DateTime.UtcNow;
                    var username = this.userService.Username;

                    if (e.State == EntityState.Deleted && 
                        e.Entity is IDeletableEntity deletableEntity)
                    {
                        deletableEntity.DeletedOn = utcNow;
                        deletableEntity.DeletedBy = username!;
                        deletableEntity.IsDeleted = true;

                        e.State = EntityState.Modified;

                        return;
                    }

                    if (e.Entity is IAuditableEntity entity)
                    {
                        if (e.State == EntityState.Added)
                        {
                            entity.CreatedOn = utcNow;
                            entity.CreatedBy = username!;
                        }
                        else if (e.State == EntityState.Modified)
                        {
                            entity.ModifiedOn = utcNow;
                            entity.ModifiedBy = username;
                        }
                    }
                });

        private static void FilterDeletedModels(ModelBuilder modelBuilder)
            => modelBuilder
                .Model
                .GetEntityTypes()
                .ToList()
                .ForEach(entityType =>
                {
                    var entityClrType = entityType.ClrType;

                    if (typeof(IDeletableEntity).IsAssignableFrom(entityClrType))
                    {
                        modelBuilder
                            .Entity(entityClrType)
                            .HasQueryFilter(GetDeletedModelsFilterExpression(entityClrType));
                    }
                });

        private static LambdaExpression? GetDeletedModelsFilterExpression(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var isDeletedProperty = Expression.Property(parameter, nameof(IDeletableEntity.IsDeleted));
            var isNotDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(false));

            return Expression.Lambda(isNotDeleted, parameter);
        }
    }
}
