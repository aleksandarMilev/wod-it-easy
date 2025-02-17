namespace WodItEasy.Application.Features.Workouts.Commands.Update
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Commands.Common;
    using Domain.Common;
    using Domain.Models.Workouts;
    using MediatR;
    
    public class UpdateWorkoutCommand : WorkoutCommand<UpdateWorkoutCommand>, IRequest<Result>
    {
        public class EditWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand, Result>
        {
            private const string NotFoundErrorMessage = "Workout not found!";
            private const string InvalidMaxParticipantsErrorMessage = "The new MaxParticipantsCount value should be greater than or equal to the CurrentParticipantsCount value!";
            private const string OverlappingErrorMessage = "A Workout is already scheduled in this date and time, please select another one!";

            private readonly IWorkoutRepository repository;

            public EditWorkoutCommandHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.repository.ById(request.Id, cancellationToken);
                
                if (workout is null)
                {
                    return NotFoundErrorMessage;
                }

                if (workout.CurrentParticipantsCount > request.MaxParticipantsCount)
                {
                    return InvalidMaxParticipantsErrorMessage;
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
