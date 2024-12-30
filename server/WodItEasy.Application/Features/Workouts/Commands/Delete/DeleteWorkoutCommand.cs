namespace WodItEasy.Application.Features.Workouts.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class DeleteWorkoutCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, Result>
        {
            private readonly IWorkoutRepository repository;

            public DeleteWorkoutCommandHandler(IWorkoutRepository repository) => this.repository = repository;

            public async Task<Result> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
                => await this.repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
