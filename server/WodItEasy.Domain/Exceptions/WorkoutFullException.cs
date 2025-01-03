namespace WodItEasy.Domain.Exceptions
{
    public class WorkoutFullException : BaseDomainException
    {
        public WorkoutFullException()
        {
        }

        public WorkoutFullException(string message) => this.Message = message;
    }
}
