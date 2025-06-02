namespace WodItEasy.EmailSender.Domain.Models
{
    public static class ModelConstants
    {
        public const int ToMinNameLength = 3;
        public const int ToMaxNameLength = 254;

        public const int SubjectMinNameLength = 1;
        public const int SubjectMaxNameLength = 150;

        public const int BodyMinNameLength = 1;
        public const int BodyMaxNameLength = 10_000;
    }
}
