namespace WodItEasy.Application.Features.Workouts.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common;
    using Domain.Factories.Workout;
    using Domain.Models.Workouts;
    using MediatR;

    public class CreateWorkoutCommand : IRequest<int>
    {
        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public int MaxParticipantsCount { get; private set; }

        public DateTime StartsAtDate { get; private set; }

        public TimeSpan StartsAtTime { get; private set; }

        public int Type { get; private set; }

        public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, int>
        {
            private readonly IWorkoutRepository repository;
            private readonly IWorkoutFactory factory;

            public CreateWorkoutCommandHandler(IWorkoutRepository repository, IWorkoutFactory factory)
            {
                this.repository = repository;
                this.factory = factory;
            }

            public async Task<int> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = this.factory
                    .WithName(request.Name)
                    .WithDescription(request.Description)
                    .WithMaxParticipantsCount(request.MaxParticipantsCount)
                    .WithStartsAtDate(request.StartsAtDate)
                    .WithStartsAtTime(request.StartsAtTime)
                    .WithType(Enumeration.FromValue<WorkoutType>(request.Type))
                    .Build();

                await this.repository.SaveAsync(workout, cancellationToken);

                return workout.Id;
            }
        }
    }
}
