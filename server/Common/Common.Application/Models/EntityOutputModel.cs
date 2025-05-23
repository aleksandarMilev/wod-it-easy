namespace WodItEasy.Common.Application.Models
{
    public class EntityOutputModel<TId>
    {
        public TId Id { get; set; } = default!;
    }
}