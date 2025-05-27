namespace WodItEasy.Profile.Application.Features.Profile.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
            => this.Include(new ProfileCommandValidator<CreateProfileCommand>());
    }
}
