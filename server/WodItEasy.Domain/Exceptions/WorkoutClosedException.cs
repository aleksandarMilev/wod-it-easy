namespace WodItEasy.Domain.Exceptions
{
    public class WorkoutClosedException : BaseDomainException
    {
        public WorkoutClosedException() { }

        public WorkoutClosedException(string message)
            => this.Message = message;
    }
}
