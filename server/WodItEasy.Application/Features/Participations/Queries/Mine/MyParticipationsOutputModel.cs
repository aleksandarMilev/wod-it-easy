namespace WodItEasy.Application.Features.Participations.Queries.Mine
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Participation;
    using Mapping;

    public class MyParticipationsOutputModel : IMapFrom<Participation>
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public string WorkoutName { get; set; } = default!;

        public DateTime WorkoutStartsAt { get; set; }

        public bool WorkoutIsFull { get; set; }

        public DateTime JoinedAt { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Status { get; set; } = default!;

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
