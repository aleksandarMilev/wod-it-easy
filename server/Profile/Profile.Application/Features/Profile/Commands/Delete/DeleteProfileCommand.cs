namespace WodItEasy.Profile.Application.Features.Profile.Commands.Delete
{
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Commands;
    using WodItEasy.Common.Application.Contracts;

    public class DeleteProfileCommand
        : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteProfileCommandHandler(
            ICurrentUserService userService,
            IProfileRepository repository)
            : IRequestHandler<DeleteProfileCommand, Result>
        {
            private const string NotFoundErrorMessage = "Profile with Id: {0} not found!";

            private readonly ICurrentUserService userService = userService;
            private readonly IProfileRepository repository = repository;

            public async Task<Result> Handle(
                DeleteProfileCommand request,
                CancellationToken cancellationToken)
            {
                var success = await this.repository.Delete(
                    this.userService.UserId!,
                    cancellationToken);

                if (success)
                    return Result.Success;

                return string.Format(
                    NotFoundErrorMessage,
                    request.Id);
            }
        }
    }
}
