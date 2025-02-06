namespace WodItEasy.Application.Features.Athlete.Queries.Get
{
    using Domain.Models.Athletes;
    using Mapping;

    public record GetAthleteOutputModel(string Name) : IMapFrom<Athlete> { }
}
