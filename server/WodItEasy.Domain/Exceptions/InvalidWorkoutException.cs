namespace WodItEasy.Domain.Exceptions
{
    using Common.Domain;

    public class InvalidWorkoutException : BaseDomainException
    {
        public InvalidWorkoutException() { }

        public InvalidWorkoutException(string message)
            => this.Message = message;
    }
}
