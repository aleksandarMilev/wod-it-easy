namespace WodItEasy.Profile.Application.Features.Profile.Commands.Common
{
    using WodItEasy.Common.Application.Commands;

    public class ProfileCommand<TCommand>
        : EntityCommand<int>
            where TCommand : EntityCommand<int>
    {
        public string DisplayName { get; set; } = default!;

        public string? AvatarUrl { get; set; } = default!;

        public string? Bio { get; set; } = default!;
    }
}
