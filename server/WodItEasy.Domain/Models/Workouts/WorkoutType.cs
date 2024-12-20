namespace WodItEasy.Domain.Models.Workouts
{
    using Common;

    public class WorkoutType : Enumeration
    {
        private static readonly WorkoutType Weightlifting = new(1, nameof(Weightlifting));
        private static readonly WorkoutType Gymnastic = new(2, nameof(Gymnastic));
        private static readonly WorkoutType Cardiovascular = new(3, nameof(Cardiovascular));
        private static readonly WorkoutType Mobility = new(4, nameof(Mobility));
        private static readonly WorkoutType Powerlifting = new(5, nameof(Powerlifting));
        private static readonly WorkoutType Crossfit = new(6, nameof(Crossfit));
        private static readonly WorkoutType Other = new(7, nameof(Other));

        public WorkoutType(int value, string name) 
            : base(value, name)
        {
        }
    }
}
