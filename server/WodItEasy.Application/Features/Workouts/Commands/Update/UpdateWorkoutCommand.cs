namespace WodItEasy.Application.Features.Workouts.Commands.Update
{
    using Commands.Common;
    using Domain.Models.Workouts;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Domain.Models;

    public class UpdateWorkoutCommand
        : WorkoutCommand<UpdateWorkoutCommand>, IRequest<Result>
    {
        public class EditWorkoutCommandHandler(
            IWorkoutRepository repository)
            : IRequestHandler<UpdateWorkoutCommand, Result>
        {
            private const string NotFoundErrorMessage = "Workout not found!";
            private const string InvalidMaxParticipantsErrorMessage = "There are {0} participants in the workout. You can't set the Max Participants value to less than that!";
            private const string OverlappingErrorMessage = "A Workout is already scheduled in this date and time, please select another one!";

            private readonly IWorkoutRepository repository = repository;

            public async Task<Result> Handle(
                UpdateWorkoutCommand request,
                CancellationToken cancellationToken)
            {
                var workout = await this.repository.ById(request.Id, cancellationToken);
                
                if (workout is null)
                {
                    return NotFoundErrorMessage;
                }

                if (workout.CurrentParticipantsCount > request.MaxParticipantsCount)
                {
                    return string.Format(
                        InvalidMaxParticipantsErrorMessage,
                        workout.CurrentParticipantsCount);
                }

                workout
                    .UpdateName(request.Name)
                    .UpdateImageUrl(request.ImageUrl)
                    .UpdateDescription(request.Description)
                    .UpdateMaxParticipantsCount(request.MaxParticipantsCount)
                    .UpdateStartsAt(DateTime.Parse(request.StartsAt).ToUniversalTime())
                    .UpdateType(Enumeration.FromValue<WorkoutType>(request.Type));

                var others = await this.repository.ByDate(
                    workout.StartsAt,
                    request.Id,
                    cancellationToken);

                if (workout.IsOverlappingExistingOne(others))
                {
                    return OverlappingErrorMessage;
                }

                await this.repository.Save(workout, cancellationToken);

                return Result.Success;
            }
        }
    }
}
