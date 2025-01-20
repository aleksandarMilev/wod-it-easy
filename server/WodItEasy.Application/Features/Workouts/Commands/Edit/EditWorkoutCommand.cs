namespace WodItEasy.Application.Features.Workouts.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Common;
    using Domain.Models.Workouts;
    using MediatR;

    public class EditWorkoutCommand : EntityCommand<int>, IRequest<Result>
    {
        public string Name { get; } = null!;

        public string Description { get; } = null!;

        public int MaxParticipantsCount { get; }

        public DateTime StartsAtDate { get; }

        public TimeSpan StartsAtTime { get; }

        public int Type { get; }

        public class EditWorkoutCommandHandler : IRequestHandler<EditWorkoutCommand, Result>
        {
            private const string NotFoundErrorMessage = "Workout not found!";

            private readonly IWorkoutRepository repository;

            public EditWorkoutCommandHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(EditWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.repository.Find(request.Id, cancellationToken);

                if (workout is null)
                {
                    return NotFoundErrorMessage;
                }

                workout
                    .UpdateName(request.Name)
                    .UpdateDescription(request.Description)
                    .UpdateMaxParticipantsCount(request.MaxParticipantsCount)
                    .UpdateStartsAtDate(request.StartsAtDate)
                    .UpdateStartsAtTime(request.StartsAtTime)
                    .UpdateType(Enumeration.FromValue<WorkoutType>(request.Type));

                await this.repository.SaveAsync(workout, cancellationToken);

                return Result.Success;
            }
        }
    }
}
