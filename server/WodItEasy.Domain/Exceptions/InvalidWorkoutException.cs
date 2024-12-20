namespace WodItEasy.Domain.Exceptions
{
    internal class InvalidWorkoutException : BaseDomainException
    {
        public InvalidWorkoutException()
        {
        }

        public InvalidWorkoutException(string message)
            => this.Message = message;
    }
}
