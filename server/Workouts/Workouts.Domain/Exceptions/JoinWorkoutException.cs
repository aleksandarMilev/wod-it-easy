namespace WodItEasy.Workouts.Domain.Exceptions
{
    using Common.Domain;

    public class JoinWorkoutException : BaseDomainException
    {
        public JoinWorkoutException() { }

        public JoinWorkoutException(string message) 
            => this.Message = message;
    }
}
