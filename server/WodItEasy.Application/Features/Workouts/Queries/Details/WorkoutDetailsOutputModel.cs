namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using AutoMapper;
    using Common;
    using Domain.Models.Workouts;
    using WodItEasy.Common.Application.Mapping;

    public class WorkoutDetailsOutputModel
        : WorkoutOutputModel, IMapFrom<Workout>
    {
        public string Description { get; set; } = default!;

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Workout, WorkoutDetailsOutputModel>()
                .IncludeBase<Workout, WorkoutOutputModel>();
    }
}
