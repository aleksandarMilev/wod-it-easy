namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts;
    using Domain.Common;

    internal class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        private readonly WodItEasyDbContext data;

        public DataRepository(WodItEasyDbContext data) => this.data = data;

        public IQueryable<TEntity> All() => this.data.Set<TEntity>();

        public Task<int> SaveChanges(CancellationToken cancellationToken = default) => this.data.SaveChangesAsync(cancellationToken);
    }
}
