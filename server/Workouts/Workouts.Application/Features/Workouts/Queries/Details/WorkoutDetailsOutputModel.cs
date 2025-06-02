namespace WodItEasy.Workouts.Application.Features.Workouts.Queries.Details
{
    using AutoMapper;
    using Common;
    using Domain.Models.Participation;
    using Domain.Models.Workouts;
    using WodItEasy.Common.Application.Mapping;

    public class WorkoutDetailsOutputModel
        : WorkoutOutputModel, IMapFrom<Workout>
    {
        public string Description { get; set; } = default!;

        public IEnumerable<string> AthleteNames { get; set; } = Enumerable.Empty<string>();

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Workout, WorkoutDetailsOutputModel>()
                .IncludeBase<Workout, WorkoutOutputModel>()
                .ForMember(
                    dest => dest.AthleteNames,
                    opt => opt.MapFrom(
                        src => src.Participations
                            .Where(p => p.Status.Value == ParticipationStatus.Joined.Value)
                            .Select(p => p.Athlete!.Name)
                            .AsEnumerable()));
    }
}
