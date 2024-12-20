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

        public class PhoneNumber
        {
            public const int MinPhoneNumberLength = 5;
            public const int MaxPhoneNumberLength = 20;

            public const string PhoneNumberFirstSymbol = "+";
        }

        public class Athlete
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 200;
        }

        public class Membership
        {
            public static readonly DateTime MinStartDateValue = DateTime.Now;
            public static readonly DateTime MaxStartDateValue = DateTime.Now.AddDays(30);
        }

        public class Workout
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 200;

            public const int MinDescriptionLength = 2;
            public const int MaxDescriptionLength = 200;

            public const int MaxParticipantsCountMinValue = 1;
            public const int MaxParticipantsCountMaxValue = 15;

            public static readonly DateTime MinStartAtDateValue = DateTime.Now;
            public static readonly DateTime MaxStartAtDateValue = DateTime.Now.AddDays(7);
        }
    }
}
