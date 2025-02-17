namespace WodItEasy.Application.Features.Athlete.Queries.Details
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Domain.Models.Athletes;
    using Domain.Models.Participation;
    using Mapping;

    public class GetAthleteDetailsOutputModel : IMapFrom<Athlete>
    {
        public string Name { get; set; } = default!;

        public int UpcomingParticipationsCount { get; set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Athlete, GetAthleteDetailsOutputModel>()
                .ForMember(
                    dest => dest.UpcomingParticipationsCount,
                    opt => opt.MapFrom(
                        src => src
                            .Participations
                            .Count(p =>
                                p.Status.Value == ParticipationStatus.Joined.Value
                                && p.Workout != null
                                && p.Workout.StartsAt > DateTime.UtcNow)));
    }
}