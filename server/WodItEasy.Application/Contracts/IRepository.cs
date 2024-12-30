namespace WodItEasy.Application.Contracts
{
    using System.Threading.Tasks;
    using System.Threading;
    using Domain.Common;

    public interface IRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
        Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
