namespace WodItEasy.Workouts.Application.Features.Workouts.Commands.Delete
{
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Commands;

    public class DeleteWorkoutCommand
        : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteWorkoutCommandHandler(
            IWorkoutRepository repository)
            : IRequestHandler<DeleteWorkoutCommand, Result>
        {
            private const string NotFoundErrorMessage = "Workout with Id: {0} not found!";

            private readonly IWorkoutRepository repository = repository;

            public async Task<Result> Handle(
                DeleteWorkoutCommand request,
                CancellationToken cancellationToken)
            {
                 var success = await this.repository.Delete(request.Id, cancellationToken);

                if (success)
                {
                    return Result.Success;
                }

                return string.Format(NotFoundErrorMessage, request.Id);
            }
        }
    }
}
