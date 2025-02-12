namespace WodItEasy.Application.Features.Workouts.Commands.Common
{
    using Application.Common;

    public abstract class WorkoutCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Name { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int MaxParticipantsCount { get; set; }

        public string StartsAtDate { get; set; } = default!;

        public string StartsAtTime { get; set; } = default!;

        public int Type { get; set; }
    }
}
