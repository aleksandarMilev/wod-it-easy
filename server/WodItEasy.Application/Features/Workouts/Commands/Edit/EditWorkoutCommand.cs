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
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int MaxParticipantsCount { get; set; }

        public string StartsAtDate { get; set; } = null!;

        public string StartsAtTime { get; set; } = null!;

        public int Type { get; set; }

        public class EditWorkoutCommandHandler : IRequestHandler<EditWorkoutCommand, Result>
        {
            private const string NotFoundErrorMessage = "Workout not found!";
            private const string OverlappingErrorMessage = "A Workout is already scheduled in this date and time, please select another one.";

            private readonly IWorkoutRepository repository;

            public EditWorkoutCommandHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(EditWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.repository.ById(request.Id, cancellationToken);
                
                if (workout is null)
                {
                    return NotFoundErrorMessage;
                }

                workout
                    .UpdateName(request.Name)
                    .UpdateImageUrl(request.ImageUrl)
                    .UpdateDescription(request.Description)
                    .UpdateMaxParticipantsCount(request.MaxParticipantsCount)
                    .UpdateStartsAtDate(DateTime.Parse(request.StartsAtDate))
                    .UpdateStartsAtTime(TimeSpan.Parse(request.StartsAtTime))
                    .UpdateType(Enumeration.FromValue<WorkoutType>(request.Type));

                var others = await this.repository.ByDate(
                    workout.StartsAtDate.Date,
                    request.Id,
                    cancellationToken);

                if (workout.IsOverlappingExistingOne(others))
                {
                    return OverlappingErrorMessage;
                }

                await this.repository.SaveAsync(workout, cancellationToken);

                return Result.Success;
            }
        }
    }
}
