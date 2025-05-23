namespace WodItEasy.Common.Infrastructure
{
    using Application.Contracts;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public abstract class DataRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(TDbContext data)
            => this.Data = data;

        protected TDbContext Data { get; }

        protected IQueryable<TEntity> All()
            => this.Data.Set<TEntity>();

        protected IQueryable<TEntity> AllAsNoTracking()
            => this.All().AsNoTracking();

        public async Task Save(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);
            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
