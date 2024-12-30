namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts;
    using Domain.Common;

    internal abstract class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(WodItEasyDbContext data) => this.Data = data;

        protected WodItEasyDbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);
            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
