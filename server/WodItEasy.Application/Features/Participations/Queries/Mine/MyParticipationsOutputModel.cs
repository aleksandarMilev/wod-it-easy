namespace WodItEasy.Application.Features.Participations.Queries.Mine
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Participation;
    using Mapping;

    public record MyParticipationsOutputModel(
        int Id,
        int WorkoutId,
        string WorkoutName,
        DateTime WorkoutStartsAtDate,
        TimeSpan WorkoutStartsAtTime,
        bool WorkoutIsFull,
        DateTime JoinedAt,
        DateTime? ModifiedOn,
        string Status) : IMapFrom<Participation>
    {
        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Participation, MyParticipationsOutputModel>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(
                        src => Enumeration.NameFromValue<ParticipationStatus>(src.Status.Value)))
                .ForMember(
                    dest => dest.WorkoutIsFull,
                    opt => opt.MapFrom(
                        src => src.Workout!.CurrentParticipantsCount == src.Workout.MaxParticipantsCount));
    }
}
