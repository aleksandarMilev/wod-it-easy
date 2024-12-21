namespace WodItEasy.Domain.Models.Workouts
{
    using Common;

    public class WorkoutType : Enumeration
    {
        public static readonly WorkoutType Weightlifting = new(1, nameof(Weightlifting));
        public static readonly WorkoutType Gymnastic = new(2, nameof(Gymnastic));
        public static readonly WorkoutType Cardiovascular = new(3, nameof(Cardiovascular));
        public static readonly WorkoutType Mobility = new(4, nameof(Mobility));
        public static readonly WorkoutType Powerlifting = new(5, nameof(Powerlifting));
        public static readonly WorkoutType CrossFit = new(6, nameof(CrossFit));
        public static readonly WorkoutType Other = new(7, nameof(Other));

        public WorkoutType(int value, string name) 
            : base(value, name)
        {
        }
    }
}
