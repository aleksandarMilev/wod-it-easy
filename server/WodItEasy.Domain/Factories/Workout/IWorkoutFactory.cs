namespace WodItEasy.Domain.Factories.Workout
{
    using Common.Domain;
    using Models.Workouts;

    public interface IWorkoutFactory : IFactory<Workout>
    {
        IWorkoutFactory WithName(string name);

        IWorkoutFactory WithImageUrl(string imageUrl);

        IWorkoutFactory WithDescription(string description);

        IWorkoutFactory WithMaxParticipantsCount(int maxParticipantsCount);

        IWorkoutFactory StartsAt(DateTime startsAt);

        IWorkoutFactory WithType(WorkoutType type);
    }
}
