namespace WodItEasy.Workouts.Application.Features.Participations.Commands.Common
{
    using WodItEasy.Common.Application.Commands;

    public abstract class ParticipationCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int> { }
}
