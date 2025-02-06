namespace WodItEasy.Application.Features.Athlete.Commands.Update
{
    using Common;
    using FluentValidation;

    public class UpdateAthleteCommandValidator : AbstractValidator<UpdateAthleteCommand>
    {
        public UpdateAthleteCommandValidator()
            => this.Include(new AthleteCommandValidator<UpdateAthleteCommand>());
    }
}
