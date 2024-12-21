namespace WodItEasy.Domain.Exceptions
{
    public class InvalidWorkoutException : BaseDomainException
    {
        public InvalidWorkoutException()
        {
        }

        public InvalidWorkoutException(string message) => this.Message = message;
    }
}
