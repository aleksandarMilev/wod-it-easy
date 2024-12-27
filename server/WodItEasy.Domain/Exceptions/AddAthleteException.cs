namespace WodItEasy.Domain.Exceptions
{
    public class AddAthleteException : BaseDomainException
    {
        public AddAthleteException()
        {
        }

        public AddAthleteException(string message) => this.Message = message;
    }
}
