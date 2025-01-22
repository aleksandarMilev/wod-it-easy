namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Workouts;
    using Mapping;

    public class SearchWorkoutOutputModel : IMapFrom<Workout>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MaxParticipantsCount { get; set; }

        public int CurrentParticipantsCount { get; set; }

        public DateTime StartsAtDate { get; set; }

        public TimeSpan StartsAtTime { get; set; }

        public string Type { get; set; } = null!;

        public void Mapping(Profile profile)
            => profile
                .CreateMap<Workout, SearchWorkoutOutputModel>()
                .ForMember(
                    dest => dest.Type, 
                    opt => opt.MapFrom(src => Enumeration.NameFromValue<WorkoutType>(src.Type.Value)));
    }
}
