namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using FakeItEasy;

    public class WorkoutFakes : IDummyFactory
    {
        public Priority Priority
            => Priority.Default;

        public bool CanCreate(Type type)
            => type == typeof(Workout);

        public object? Create(Type type)
            => new Workout(
                "Test",
                "https://test-image.com",
                "A Test Test Test",
                10,
                DateTime.UtcNow.AddDays(1).Add(TimeSpan.FromHours(12)),
                WorkoutType.CrossFit);
    }
}
