namespace WodItEasy.Application.Features.Athlete.Commands.Common
{
    using Application.Common;

    public abstract class AthleteCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Name { get; set; } = default!;
    }
}
