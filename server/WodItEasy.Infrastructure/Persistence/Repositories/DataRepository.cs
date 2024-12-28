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
        private readonly WodItEasyDbContext data;

        protected DataRepository(WodItEasyDbContext data) => this.data = data;

        protected IQueryable<TEntity> All() => this.data.Set<TEntity>();

        protected Task<int> SaveChanges(CancellationToken cancellationToken = default) => this.data.SaveChangesAsync(cancellationToken);
    }
}
