namespace WodItEasy.Common.Application.Contracts
{
    using Domain.Models;

    public interface IRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
        Task Save(
            TEntity entity, 
            CancellationToken cancellationToken = default);
    }
}
