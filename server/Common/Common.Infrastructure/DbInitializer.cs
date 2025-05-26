namespace WodItEasy.Common.Infrastructure
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Domain.Models;

    public abstract class DbInitializer : IDbInitializer
    {
        private readonly DbContext data;
        private readonly IEnumerable<IInitialData> initialDataProviders;

        protected internal DbInitializer(DbContext data)
        {
            this.data = data;
            this.initialDataProviders = [];
        }

        protected internal DbInitializer(
            DbContext data,
            IEnumerable<IInitialData> initialDataProviders)
            : this(data)
            => this.initialDataProviders = initialDataProviders;

        public virtual async Task Initialize()
        {
            await this.data.Database.MigrateAsync();

            foreach (var initialDataProvider in this.initialDataProviders)
            {
                if (this.DataSetIsEmpty(initialDataProvider.EntityType))
                {
                    var data = initialDataProvider.GetData();

                    foreach (var entity in data)
                        this.data.Add(entity);
                }
            }

            await this.data.SaveChangesAsync();
        }

        private bool DataSetIsEmpty(Type type)
        {
            var setMethod = typeof(DbInitializer)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(m =>
                    m.Name == nameof(GetSet) &&
                    m.IsGenericMethodDefinition &&
                    m.GetGenericArguments().Length == 1)
                    ?? throw new InvalidOperationException(
                        "Failed to find generic method GetSet<TEntity>.");

            var genericSetMethod = setMethod.MakeGenericMethod(type);
            var set = genericSetMethod.Invoke(this, [])
                ?? throw new InvalidOperationException(
                    $"Failed to get DbSet for type {type.FullName}.");

            var countMethod = typeof(Queryable)
                .GetMethods()
                .First(m => 
                    m.Name == nameof(Queryable.Count) &&
                    m.GetParameters().Length == 1)
                .MakeGenericMethod(type);

            var result = (int)countMethod.Invoke(null, new[] { set })!;

            return result == 0;
        }


        private DbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
            => this.data.Set<TEntity>();
    }
}
