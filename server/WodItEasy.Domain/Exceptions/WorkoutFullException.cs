namespace WodItEasy.Domain.Exceptions
{
    using Common.Domain;

    public class WorkoutFullException : BaseDomainException
    {
        public WorkoutFullException() { }

        public WorkoutFullException(string message)
            => this.Message = message;
    }
}
