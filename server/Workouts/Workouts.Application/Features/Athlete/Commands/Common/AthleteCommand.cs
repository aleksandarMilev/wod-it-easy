namespace WodItEasy.Workouts.Application.Features.Athlete.Commands.Common
{
    using WodItEasy.Common.Application.Commands;

    public abstract class AthleteCommand<TCommand>
        : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Name { get; set; } = default!;
    }
}
