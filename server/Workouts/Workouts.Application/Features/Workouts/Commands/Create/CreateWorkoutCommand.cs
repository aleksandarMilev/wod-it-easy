﻿namespace WodItEasy.Workouts.Application.Features.Workouts.Commands.Create
{
    using Commands.Common;
    using Domain.Factories.Workout;
    using Domain.Models.Workouts;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Domain.Models;

    public class CreateWorkoutCommand
        : WorkoutCommand<CreateWorkoutCommand>, IRequest<Result<CreateWorkoutOutputModel>>
    {
        public class CreateWorkoutCommandHandler(
            IWorkoutRepository repository,
            IWorkoutFactory factory)
            : IRequestHandler<CreateWorkoutCommand, Result<CreateWorkoutOutputModel>>
        {
            private const string OverlappingErrorMessage = "A Workout is already scheduled in this date and time, please select another one.";

            private readonly IWorkoutRepository repository = repository;
            private readonly IWorkoutFactory factory = factory;

            public async Task<Result<CreateWorkoutOutputModel>> Handle(
                CreateWorkoutCommand request, 
                CancellationToken cancellationToken = default)
            {
                var workout = this.factory
                    .WithName(request.Name)
                    .WithImageUrl(request.ImageUrl)
                    .WithDescription(request.Description)
                    .WithMaxParticipantsCount(request.MaxParticipantsCount)
                    .StartsAt(DateTime.Parse(request.StartsAt).ToUniversalTime())
                    .WithType(Enumeration.FromValue<WorkoutType>(request.Type))
                    .Build();

                var others = await this.repository.ByDate(
                    workout.StartsAt,
                    null,
                    cancellationToken);

                if (workout.IsOverlappingExistingOne(others))
                {
                    return OverlappingErrorMessage;
                }

                await this.repository.Save(workout, cancellationToken);

                return new CreateWorkoutOutputModel() { Id = workout.Id };
            }
        }
    }
}
