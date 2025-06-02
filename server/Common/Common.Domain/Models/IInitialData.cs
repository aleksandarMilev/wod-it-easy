namespace WodItEasy.Common.Domain.Models
{
    public interface IInitialData
    {
        Type EntityType { get; }

        IEnumerable<object> GetData();
    }
}
