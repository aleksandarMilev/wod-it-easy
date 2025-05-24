namespace WodItEasy.Workouts.Application.Features.Workouts.Queries.Search
{
    using AutoMapper;
    using Common;
    using Domain.Models.Workouts;
    using WodItEasy.Common.Application.Mapping;

    public class SearchWorkoutOutputModel
        : WorkoutOutputModel, IMapFrom<Workout>
    {
        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Workout, SearchWorkoutOutputModel>()
                .IncludeBase<Workout, WorkoutOutputModel>();
    }
}
