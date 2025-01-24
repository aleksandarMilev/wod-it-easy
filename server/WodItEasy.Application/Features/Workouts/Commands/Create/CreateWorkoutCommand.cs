namespace WodItEasy.Application.Features.Workouts.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Domain.Common;
    using Domain.Factories.Workout;
    using Domain.Models.Workouts;
    using MediatR;

    public class CreateWorkoutCommand : IRequest<Result<int>>
    {
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int MaxParticipantsCount { get; set; }

        public string StartsAtDate { get; set; } = null!;

        public string StartsAtTime { get; set; } = null!;

        public int Type { get; set; }

        public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, Result<int>>
        {
            private const string OverlappingErrorMessage = "A Workout is already scheduled in this date and time, please select another one.";

            private readonly IWorkoutRepository repository;
            private readonly IWorkoutFactory factory;

            public CreateWorkoutCommandHandler(IWorkoutRepository repository, IWorkoutFactory factory)
            {
                this.repository = repository;
                this.factory = factory;
            }

            public async Task<Result<int>> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = this.factory
                    .WithName(request.Name)
                    .WithImageUrl(request.ImageUrl)
                    .WithDescription(request.Description)
                    .WithMaxParticipantsCount(request.MaxParticipantsCount)
                    .WithStartsAtDate(DateTime.Parse(request.StartsAtDate))
                    .WithStartsAtTime(TimeSpan.Parse(request.StartsAtTime))
                    .WithType(Enumeration.FromValue<WorkoutType>(request.Type))
                    .Build();

                var others = await this.repository.ByDate(
                    workout.StartsAtDate.Date,
                    null,
                    cancellationToken);

                if (workout.IsOverlappingExistingOne(others))
                {
                    return OverlappingErrorMessage;
                }

                await this.repository.SaveAsync(workout, cancellationToken);

                return workout.Id;
            }
        }
    }
}
