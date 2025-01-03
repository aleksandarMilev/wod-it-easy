namespace WodItEasy.Domain.Models
{
    using System;

    public static class ModelConstants
    {
        public class Common
        {
            public const int MaxUrlLength = 2_048;
            public const int Zero = 0;
        }

        public class PhoneNumberConstants
        {
            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 20;

            public const string PhoneNumberFirstSymbol = "+";
        }

        public class AthleteConstants
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 100;

            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 256;
        }

        public class MembershipConstants
        {
            public static readonly DateTime MinStartDateValue = DateTime.Now.AddMinutes(-1);
            public static readonly DateTime MaxStartDateValue = DateTime.Now.AddDays(30);

            public const int MinWorkoutsCountValue = 1;
            public const int MaxWorkoutsCountValue = 31;
        }

        public class WorkoutConstants
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 100;

            public const int MinDescriptionLength = 2;
            public const int MaxDescriptionLength = 500;

            public const int MaxParticipantsCountMinValue = 1;
            public const int MaxParticipantsCountMaxValue = 15;

            public static readonly DateTime MinStartAtDateValue = DateTime.Now.AddMinutes(-1);
            public static readonly DateTime MaxStartAtDateValue = DateTime.Now.AddDays(7);
        }
    }
}
