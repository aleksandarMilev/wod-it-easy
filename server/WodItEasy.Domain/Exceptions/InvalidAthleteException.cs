namespace WodItEasy.Domain.Exceptions
{
    public class InvalidAthleteException : BaseDomainException
    {
        public InvalidAthleteException() { }

        public InvalidAthleteException(string message)
            => this.Message = message;
    }
}
