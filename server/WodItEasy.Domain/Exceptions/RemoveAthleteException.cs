namespace WodItEasy.Domain.Exceptions
{
    public class RemoveAthleteException : BaseDomainException
    {
        public RemoveAthleteException()
        {
        }

        public RemoveAthleteException(string message) => this.Message = message;
    }
}
