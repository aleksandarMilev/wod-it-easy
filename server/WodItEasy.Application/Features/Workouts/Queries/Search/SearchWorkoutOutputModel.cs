namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Workouts;
    using Mapping;

    public class SearchWorkoutOutputModel : IMapFrom<Workout>
    {
        public int Id { get; }

        public string Name { get; } = null!;

        public int MaxParticipantsCount { get; }

        public int CurrentParticipantsCount { get; }

        public DateTime StartsAtDate { get; }

        public TimeSpan StartsAtTime { get; }

        public string Type { get; } = null!;

        public void Mapping(Profile profile)
            => profile
                .CreateMap<Workout, SearchWorkoutOutputModel>()
                .ForMember(
                    dest => dest.Type, 
                    opt => opt.MapFrom(src => Enumeration.NameFromValue<WorkoutType>(src.Type.Value)));
    }
}
