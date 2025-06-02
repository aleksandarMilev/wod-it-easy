namespace WodItEasy.Workouts.Domain.Models
{
    public static class ModelConstants
    {
        public class Common
        {
            public const int UrlMaxLength = 2_048;
            public const int Zero = 0;
        }

        public class AthleteConstants
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 100;

            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 256;
        }

        public class WorkoutConstants
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 100;

            public const int MinDescriptionLength = 2;
            public const int MaxDescriptionLength = 500;

            public const int MaxParticipantsCountMinValue = 1;
            public const int MaxParticipantsCountMaxValue = 15;
        }
    }
}
