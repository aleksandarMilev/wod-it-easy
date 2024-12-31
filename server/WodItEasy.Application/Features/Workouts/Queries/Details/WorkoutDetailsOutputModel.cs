namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Workouts;
    using Mapping;

    public class WorkoutDetailsOutputModel : IMapFrom<Workout>
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public int MaxParticipantsCount { get; init; }

        public int CurrentParticipantsCount { get; init; }

        public DateTime StartsAtDate { get; init; }

        public TimeSpan StartsAtTime { get; init; }

        public string Type { get; init; } = null!;

        public void Mapping(Profile profile)
            => profile
                .CreateMap<Workout, WorkoutDetailsOutputModel>()
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(src => Enumeration.NameFromValue<WorkoutType>(src.Type.Value)));
    }
}
