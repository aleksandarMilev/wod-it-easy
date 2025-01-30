namespace WodItEasy.Application.Features.Participations.Queries.Mine
{
    using System;
    using Domain.Models.Participation;
    using Mapping;

    public class MyParticipationsOutputModel : IMapFrom<Participation>
    {
        public int WorkoutId { get; set; }

        public string WorkoutName { get; set; } = default!;

        public DateTime WorkoutStartsAtDate { get; set; }

        public TimeSpan WorkoutStartsAtTime { get; set; }

        public DateTime JoinedAt { get; set; }

        public ParticipationStatus Status { get; set; } = default!;
    }
}
