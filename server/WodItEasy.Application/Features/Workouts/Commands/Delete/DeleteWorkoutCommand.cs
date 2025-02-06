namespace WodItEasy.Application.Features.Workouts.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class DeleteWorkoutCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, Result>
        {
            private const string NotFoundErrorMessage = "Workout with Id: {0} not found!";

            private readonly IWorkoutRepository repository;

            public DeleteWorkoutCommandHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
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
