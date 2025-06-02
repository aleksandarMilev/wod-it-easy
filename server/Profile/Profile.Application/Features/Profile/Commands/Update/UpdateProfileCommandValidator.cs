namespace WodItEasy.Profile.Application.Features.Profile.Commands.Update
{
    using Common;
    using FluentValidation;
    public class UpdateWorkoutCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateWorkoutCommandValidator()
            => this.Include(new ProfileCommandValidator<UpdateProfileCommand>());
    }
}
