namespace WodItEasy.Profile.Infrastructure.Persistence
{
    using System.Linq.Expressions;
    using System.Reflection;
    using Common.Application.Contracts;
    using Common.Domain.Models;
    using Microsoft.EntityFrameworkCore;

    internal class ProfileDbContext(
        DbContextOptions<ProfileDbContext> options,
        ICurrentUserService userService)
        : DbContext(options)
    {
        private readonly ICurrentUserService userService = userService;

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfo();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo();

            return base.SaveChangesAsync(
                acceptAllChangesOnSuccess,
                cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            FilterModels(modelBuilder);
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
                        deletableEntity.DeletedBy = username;
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

        private static void FilterModels(ModelBuilder modelBuilder)
            => modelBuilder
                .Model
                .GetEntityTypes()
                .ToList()
                .ForEach(entityType =>
                {
                    var entityClrType = entityType.ClrType;
                    if (typeof(IDeletableEntity).IsAssignableFrom(entityClrType))
                    {
                        var filterExpression = GetDeletedEntitiesFilterExpression(entityClrType);
                        modelBuilder.Entity(entityClrType).HasQueryFilter(filterExpression);
                    }
                });

        private static LambdaExpression? GetDeletedEntitiesFilterExpression(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var isDeletedProperty = Expression.Property(
                parameter,
                nameof(IDeletableEntity.IsDeleted));

            var isNotDeleted = Expression.Equal(
                isDeletedProperty,
                Expression.Constant(false));

            return Expression.Lambda(isNotDeleted, parameter);
        }
    }
}
