namespace WodItEasy.Domain.Models.Participation
{
    using System;
    using Athletes;
    using FakeItEasy;
    using Workouts;

    public class ParticipationFakes
    {
        public class ParticipationDummyFactory : IDummyFactory
        {
            public Priority Priority
                => Priority.Default;

            public bool CanCreate(Type type)
                => type == typeof(Participation);

            public object? Create(Type type)
            {
                var athlete = new Athlete("Some Athlete", "userId");

                var workout = new Workout(
                    "Test",
                    "https://test-url.com/some-image",
                    "Description",
                    10,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    WorkoutType.CrossFit);

                return new Participation(
                    athlete,
                    workout,
                    DateTime.Now,
                    ParticipationStatus.Joined);
            }
        }
    }
}
