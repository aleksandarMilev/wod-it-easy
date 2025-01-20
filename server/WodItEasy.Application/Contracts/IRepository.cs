namespace WodItEasy.Application.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common;

    public interface IRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
        Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
