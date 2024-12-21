namespace WodItEasy.Domain.Exceptions
{
    internal class AddAthleteException : BaseDomainException
    {
        public AddAthleteException()
        {
        }

        public AddAthleteException(string message) => this.Message = message;
    }
}
