namespace WodItEasy.Application.Features
{
    public class EntityQuery<TId>
    {
        public TId Id { get; set; } = default!;
    }

    public static class EntityQueryExtensions
    {
        public static TQuery SetId<TQuery, TId>(this TQuery query, TId id)
            where TQuery : EntityQuery<TId>
        {
            query.Id = id;

            return query;
        }
    }
}