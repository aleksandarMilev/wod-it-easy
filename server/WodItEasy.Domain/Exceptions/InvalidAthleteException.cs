namespace WodItEasy.Domain.Exceptions
{
    using Common.Domain;

    public class InvalidAthleteException : BaseDomainException
    {
        public InvalidAthleteException() { }

        public InvalidAthleteException(string message)
            => this.Message = message;
    }
}
