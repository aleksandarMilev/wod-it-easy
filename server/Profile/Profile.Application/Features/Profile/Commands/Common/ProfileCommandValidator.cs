namespace WodItEasy.Profile.Application.Features.Profile.Commands.Common
{
    using FluentValidation;
    using WodItEasy.Common.Application.Commands;

    using static Domain.Models.ModelConstants.ProfileConstants;
    using static WodItEasy.Common.Domain.Constants;

    public class ProfileCommandValidator<TCommand>
        : AbstractValidator<ProfileCommand<TCommand>>
            where TCommand : EntityCommand<int>
    {
        public ProfileCommandValidator()
        {
            this
                .RuleFor(p => p.AvatarUrl)
                .MinimumLength(UrlMinLength)
                .MaximumLength(UrlMaxLength)
                .When(p => !string.IsNullOrWhiteSpace(p.AvatarUrl))
                .WithMessage(
                    $"Avatar URL must be between {UrlMinLength} and {UrlMaxLength} characters.");

            this
                .RuleFor(p => p.Bio)
                .MinimumLength(MinBioLength)
                .MaximumLength(MaxBioLength)
                .When(p => !string.IsNullOrWhiteSpace(p.Bio))
                .WithMessage(
                    $"Bio must be between {MinBioLength} and {MaxBioLength} characters.");
        }
    }
}
