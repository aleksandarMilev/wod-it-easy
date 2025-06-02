namespace WodItEasy.Workouts.Application.Features.Athlete.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateAthleteCommandValidator : AbstractValidator<CreateAthleteCommand>
    {
        public CreateAthleteCommandValidator()
            => this.Include(new AthleteCommandValidator<CreateAthleteCommand>());
    }
}
