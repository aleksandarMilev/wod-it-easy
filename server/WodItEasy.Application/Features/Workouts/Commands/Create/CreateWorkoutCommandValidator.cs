namespace WodItEasy.Application.Features.Workouts.Commands.Create
{
    using System;
    using FluentValidation;

    using static Domain.Models.ModelConstants.WorkoutConstants;

    public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
    {
        public CreateWorkoutCommandValidator()
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
                .WithMessage($"MaxParticipantsCount must have between {MaxParticipantsCountMinValue} and {MaxParticipantsCountMaxValue} symbols.");

            this
                .RuleFor(w => DateTime.Parse(w.StartsAtDate))
                .LessThanOrEqualTo(MaxStartAtDateValue)
                .GreaterThanOrEqualTo(MinStartAtDateValue)
                .WithMessage($"StartDate must be between {MinStartAtDateValue:O} and {MaxStartAtDateValue:O}.");
        }
    }
}
