namespace WodItEasy.Application.Features.Athlete.Queries.Get
{
    using AutoMapper;
    using Domain.Models.Athletes;
    using Mapping;

    public class GetAthleteOutputModel : IMapFrom<Athlete>
    {
        public string Name { get; set; } = default!;

        public int ParticipationsCount { get; set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Athlete, GetAthleteOutputModel>()
                .ForMember(
                    dest => dest.ParticipationsCount,
                    opt => opt.MapFrom(src => src.Participations.Count));
    }
}
