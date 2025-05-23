namespace WodItEasy.Domain.Exceptions
{
    using Common.Domain;

    public class WorkoutClosedException : BaseDomainException
    {
        public WorkoutClosedException() { }

        public WorkoutClosedException(string message)
            => this.Message = message;
    }
}
