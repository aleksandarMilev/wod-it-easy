namespace WodItEasy.Infrastructure.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Domain.Common;
    using Microsoft.EntityFrameworkCore;

    internal class WodItEasyDbInitializer : IInitializer
    {
        private readonly WodItEasyDbContext data;
        private readonly IEnumerable<IInitialData> initialDataProviders;

        public WodItEasyDbInitializer(
            WodItEasyDbContext data,
            IEnumerable<IInitialData> initialDataProviders)
        {
            this.data = data;
            this.initialDataProviders = initialDataProviders;
        }

        public void Initialize()
        {
            // when run in docker, migrate() will be invoked before the db is freshly loaded, which causes bugs (trying to create a new db even though it already exists).
            // so, the smartest thing i came up with was thread.sleep, pls do not kill me..
            Thread.Sleep(8_000);

            this.data.Database.Migrate();

            foreach (var initialDataProvider in this.initialDataProviders)
            {
                if (this.DataSetIsEmpty(initialDataProvider.EntityType))
                {
                    var data = initialDataProvider.GetData();

                    foreach (var entity in data)
                    {
                        this.data.Add(entity);
                    }
                }
            }

            this.data.SaveChanges();
        }

        private bool DataSetIsEmpty(Type type)
        {
            var setMethod = this.GetType()
                .GetMethod(nameof(this.GetSet), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(type);

            var set = setMethod.Invoke(this, []);

            var countMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
                .MakeGenericMethod(type);

            var result = (int)countMethod.Invoke(null, [set])!;

            return result == 0;
        }

        private DbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
            => this.data.Set<TEntity>();
    }
}
