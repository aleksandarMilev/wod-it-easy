namespace WodItEasy.Workouts.Application.Features.Workouts.Commands.Common
{
    using FluentValidation;
    using WodItEasy.Common.Application.Commands;

    using static Domain.Models.ModelConstants.WorkoutConstants;

    public class WorkoutCommandValidator<TCommand>
        : AbstractValidator<WorkoutCommand<TCommand>> 
            where TCommand : EntityCommand<int>
    {
        public WorkoutCommandValidator()
        {
            this
               .RuleFor(w => w.Name)
               .NotEmpty()
               .WithMessage("Name cannot be null ot empty.")
               .Length(MinNameLength, MaxNameLength)
               .WithMessage($"Name must have between {MinNameLength} and {MaxNameLength} symbols.");

            this
                .RuleFor(w => w.Description)
                .NotEmpty()
                .WithMessage("Description  cannot be null ot empty.")
                .Length(MinDescriptionLength, MaxDescriptionLength)
                .WithMessage($"Description must have between {MinNameLength} and {MaxNameLength} symbols.");

            this
                .RuleFor(w => w.MaxParticipantsCount)
                .LessThanOrEqualTo(MaxParticipantsCountMaxValue)
                .GreaterThanOrEqualTo(MaxParticipantsCountMinValue)
                .WithMessage($"MaxParticipantsCount should be between {MaxParticipantsCountMinValue} and {MaxParticipantsCountMaxValue}.");
        }
    }
}
