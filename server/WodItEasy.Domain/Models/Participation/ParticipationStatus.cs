namespace WodItEasy.Domain.Models.Participation
{
    using Common.Domain.Models;

    public class ParticipationStatus : Enumeration
    {
        public static readonly ParticipationStatus Joined = new(1, nameof(Joined));
        public static readonly ParticipationStatus Left = new(2, nameof(Left));

        private ParticipationStatus(int value, string name)
           : base(value, name) { }

        // Ef Core needs it
        private ParticipationStatus(int value)
            : this(value, FromValue<ParticipationStatus>(value).Name) { }
    }
}
