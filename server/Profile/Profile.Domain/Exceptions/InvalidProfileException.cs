namespace WodItEasy.Profile.Domain.Exceptions
{
    using Common.Domain;

    public class InvalidProfileException : BaseDomainException
    {
        public InvalidProfileException() { }

        public InvalidProfileException(string message)
            => this.Message = message;
    }
}
