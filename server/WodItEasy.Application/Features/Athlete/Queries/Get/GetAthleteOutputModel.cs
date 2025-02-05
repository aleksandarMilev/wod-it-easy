namespace WodItEasy.Application.Features.Athlete.Queries.Get
{
    using Domain.Models.Athletes;
    using Mapping;

    public class GetAthleteOutputModel : IMapFrom<Athlete>
    {
        public string Name { get; private set; } = null!;
    }
}
