namespace WodItEasy.Workouts.Application.Features.Identity.Commands.Register
{
    using FluentValidation;

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private const int UsernameMinLength = 3;
        private const int UsernameMaxLength = 50;

        private const int EmailMinLength = 5;
        private const int EmailMaxLength = 254;

        private const int PasswordMinLength = 6;
        private const int PasswordMaxLength = 128;

        public RegisterCommandValidator()
        {
            this
                .RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty.")
                .MinimumLength(UsernameMinLength)
                .WithMessage($"Username must be at least {UsernameMinLength} characters long.")
                .MaximumLength(UsernameMaxLength)
                .WithMessage($"Username must not exceed {UsernameMaxLength} characters.");

            this
                .RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty.")
                .MinimumLength(EmailMinLength)
                .WithMessage($"Email must be at least {EmailMinLength} characters long.")
                .MaximumLength(EmailMaxLength)
                .WithMessage($"Email must not exceed {EmailMaxLength} characters.");

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
