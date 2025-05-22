namespace WodItEasy.Common.Domain
{
    using Models;

    public interface IFactory<out TEntity>
       where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
