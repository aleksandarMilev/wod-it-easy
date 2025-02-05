namespace WodItEasy.Application.Features.Participations.Queries.Mine
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Participation;
    using Mapping;

    public class MyParticipationsOutputModel : IMapFrom<Participation>
    {
        public int Id { get; private set; }

        public int WorkoutId { get; private set; }

        public string WorkoutName { get; private set; } = default!;

        public DateTime WorkoutStartsAtDate { get; private set; }

        public TimeSpan WorkoutStartsAtTime { get; private set; }

        public bool WorkoutIsFull { get; private set; }

        public DateTime JoinedAt { get; private set; }

        public DateTime? ModifiedOn { get; private set; }

        public string Status { get; private set; } = default!;

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
