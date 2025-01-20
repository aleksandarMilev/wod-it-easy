namespace WodItEasy.Application.Features.Workouts.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;

    public class DeleteWorkoutCommand : IRequest<Result>
    {
        public DeleteWorkoutCommand(int id)
            => this.Id = id;

        public int Id { get; }

        public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, Result>
        {
            private readonly IWorkoutRepository repository;

            public DeleteWorkoutCommandHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
                => await this.repository.Delete(request.Id, cancellationToken);
        }
    }
}
