namespace WodItEasy.Workouts.Application.Features.Workouts.Commands.Update
{
    using Common;
    using FluentValidation;

    public class UpdateWorkoutCommandValidator : AbstractValidator<UpdateWorkoutCommand>
    {
        public UpdateWorkoutCommandValidator()
            => this.Include(new WorkoutCommandValidator<UpdateWorkoutCommand>());
    }
}
