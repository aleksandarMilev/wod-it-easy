namespace WodItEasy.Domain.Exceptions
{
    internal class RemoveAthleteException : BaseDomainException
    {
        public RemoveAthleteException()
        {
        }

        public RemoveAthleteException(string message)
            => this.Message = message;
    }
}
