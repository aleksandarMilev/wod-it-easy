namespace WodItEasy.Application.Features.Participations.Commands.Common
{
    public abstract class ParticipationCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int> { }
}
