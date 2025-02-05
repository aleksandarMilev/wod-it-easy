namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Workouts;
    using Mapping;

    public class WorkoutDetailsOutputModel : IMapFrom<Workout>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public string ImageUrl { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public int MaxParticipantsCount { get; private set; }

        public int CurrentParticipantsCount { get; private set; }

        public DateTime StartsAtDate { get; private set; }

        public TimeSpan StartsAtTime { get; private set; }

        public string Type { get; private set; } = null!;

        public void Mapping(Profile profile)
            => profile
                .CreateMap<Workout, WorkoutDetailsOutputModel>()
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(
                        src => Enumeration.NameFromValue<WorkoutType>(src.Type.Value)));
    }
}
