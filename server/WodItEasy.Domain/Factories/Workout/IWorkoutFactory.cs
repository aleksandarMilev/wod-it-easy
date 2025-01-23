namespace WodItEasy.Domain.Factories.Workout
{
    using System;
    using Models.Workouts;

    public interface IWorkoutFactory : IFactory<Workout>
    {
        IWorkoutFactory WithName(string name);

        IWorkoutFactory WithImageUrl(string imageUrl);

        IWorkoutFactory WithDescription(string description);

        IWorkoutFactory WithMaxParticipantsCount(int maxParticipantsCount);

        IWorkoutFactory WithStartsAtDate(DateTime startsAtDate);

        IWorkoutFactory WithStartsAtTime(TimeSpan startsAtTime);

        IWorkoutFactory WithType(WorkoutType type);
    }
}
