namespace WodItEasy.Domain.Exceptions
{
    internal class InvalidAthleteException : BaseDomainException
    {
        public InvalidAthleteException()
        {
        }

        public InvalidAthleteException(string message) 
            => this.Message = message;
    }
}
