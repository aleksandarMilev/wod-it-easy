namespace WodItEasy.Domain.Models.Athletes
{
    using System;
    using FakeItEasy;

    using static ModelConstants;

    public class AthleteFakes
    {
        public class AthleteDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Athlete);

            public object? Create(Type type)
                => new Athlete(
                    "name",
                    new Membership(
                        MembershipType.FixedWorkouts,
                        DateTime.Now,
                        21));
        }
    }
}
