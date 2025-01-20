namespace WodItEasy.Domain.Exceptions
{
    public class JoinWorkoutException : BaseDomainException
    {
        public JoinWorkoutException() { }

        public JoinWorkoutException(string message) 
            => this.Message = message;
    }
}
