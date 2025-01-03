namespace WodItEasy.Domain.Models.Workouts
{
    using System;
    using FakeItEasy;

    public class WorkoutFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Workout);

        public object? Create(Type type)
            => new Workout(
                "Test",
                "A Test Test Test",
                10,
                DateTime.Now.AddDays(1),
                TimeSpan.FromMinutes(60),
                WorkoutType.CrossFit);
    }
}
