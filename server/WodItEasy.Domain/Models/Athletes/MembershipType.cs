namespace WodItEasy.Domain.Models.Athletes
{
    using Common;

    public class MembershipType : Enumeration
    {
        public static readonly MembershipType Monthly = new(1, nameof(Monthly));
        public static readonly MembershipType FixedWorkouts = new(2, nameof(FixedWorkouts));

        private MembershipType(int value, string name)
            : base(value, name)
        {
        }
    }
}
