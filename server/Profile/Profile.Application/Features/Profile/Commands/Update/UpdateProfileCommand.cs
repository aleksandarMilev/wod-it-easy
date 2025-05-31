namespace WodItEasy.Profile.Application.Features.Profile.Commands.Update
{
    using Common;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Contracts;

    public class UpdateProfileCommand
        : ProfileCommand<UpdateProfileCommand>, IRequest<Result>
    {
        public class EditProfileCommandHandler(
            ICurrentUserService userService,
            IProfileRepository repository)
            : IRequestHandler<UpdateProfileCommand, Result>
        {
            private const string NotFoundErrorMessage = "Profile with Id: {0} was not found!";

            private readonly IProfileRepository repository = repository;
            private readonly ICurrentUserService userService = userService;

            public async Task<Result> Handle(
                UpdateProfileCommand request,
                CancellationToken cancellationToken)
            {
                var profile = await this.repository.ById(
                    this.userService.UserId!,
                    cancellationToken);

                if (profile is null)
                    return string.Format(NotFoundErrorMessage, request.Id);

                profile
                    .UpdateDisplayName(request.DisplayName)
                    .UpdateAvatarUrl(request.AvatarUrl)
                    .UpdateBio(request.Bio);

                await this.repository.Save(profile, cancellationToken);

                return Result.Success;
            }
        }
    }
}
