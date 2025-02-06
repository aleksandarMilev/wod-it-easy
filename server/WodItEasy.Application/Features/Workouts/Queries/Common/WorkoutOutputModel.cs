namespace WodItEasy.Application.Features.Workouts.Queries.Common
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Workouts;
    using Mapping;

    public record WorkoutOutputModel(
        int Id,
        string Name,
        string ImageUrl,
        int MaxParticipantsCount,
        int CurrentParticipantsCount,
        DateTime StartsAtDate,
        TimeSpan StartsAtTime,
        string Type) : IMapFrom<Workout>
    {
        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Workout, WorkoutOutputModel>()
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(
                         src => Enumeration.NameFromValue<WorkoutType>(
                            src.Type != null
                                ? src.Type.Value
                                : WorkoutType.CrossFit.Value)));
    }
}
