namespace WodItEasy.Application.Features.Workouts.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
    {
        public CreateWorkoutCommandValidator()
            => this.Include(new WorkoutCommandValidator<CreateWorkoutCommand>());
    }
}
