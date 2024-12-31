namespace WodItEasy.Application.Features.Workouts.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Domain.Common;
    using Domain.Models.Workouts;
    using MediatR;

    public class EditWorkoutCommand : EntityCommand<int>, IRequest<Result>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int MaxParticipantsCount { get; set; }

        public DateTime StartsAtDate { get; set; }

        public TimeSpan StartsAtTime { get; set; }

        public int Type { get; set; }

        public class EditWorkoutCommandHandler : IRequestHandler<EditWorkoutCommand, Result>
        {
            private readonly IWorkoutRepository repository;

            public EditWorkoutCommandHandler(IWorkoutRepository repository) => this.repository = repository;

            public async Task<Result> Handle(EditWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.repository.FindAsync(request.Id, cancellationToken);

                if (workout is null)
                {
                    return "Workout not found!";
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
