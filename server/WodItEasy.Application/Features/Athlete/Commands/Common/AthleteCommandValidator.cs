namespace WodItEasy.Application.Features.Athlete.Commands.Common
{
    using FluentValidation;
    using WodItEasy.Common.Application.Commands;

    using static Domain.Models.ModelConstants.AthleteConstants;

    public class AthleteCommandValidator<TCommand>
        : AbstractValidator<AthleteCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public AthleteCommandValidator()
        {
            this
                .RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name cannot be null ot empty.")
                .Length(MinNameLength, MaxNameLength)
                .WithMessage($"Name must have between {MinNameLength} and {MaxNameLength} symbols.");
        }
    }
}
