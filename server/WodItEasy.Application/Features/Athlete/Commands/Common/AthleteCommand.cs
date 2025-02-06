namespace WodItEasy.Application.Features.Athlete.Commands.Common
{
    public abstract class AthleteCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Name { get; set; } = default!;
    }
}
