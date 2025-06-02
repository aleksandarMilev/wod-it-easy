namespace WodItEasy.Identity.Application.Commands.Login
{
    using FluentValidation;

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private const int CredentialsMinLength = 1;
        private const int CredentialsMaxLength = 255;

        private const int PasswordMinLength = 6;
        private const int PasswordMaxLength = 128;

        public LoginCommandValidator()
        {
            this
                .RuleFor(u => u.Credentials)
                .NotEmpty()
                .WithMessage("Credentials cannot be empty.")
                .MinimumLength(CredentialsMinLength)
                .WithMessage($"Credentials must be at least {CredentialsMinLength} characters long.")
                .MaximumLength(CredentialsMaxLength)
                .WithMessage($"Credentials must not exceed {CredentialsMaxLength} characters.");

            this
                .RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .MinimumLength(PasswordMinLength)
                .WithMessage($"Password must be at least {PasswordMinLength} characters long.")
                .MaximumLength(PasswordMaxLength)
                .WithMessage($"Password must not exceed {PasswordMaxLength} characters.");
        }
    }
}
