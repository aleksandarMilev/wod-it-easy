namespace WodItEasy.Domain.Exceptions
{
    public class LeaveWorkoutException : BaseDomainException
    {
        public LeaveWorkoutException() { }

        public LeaveWorkoutException(string message)
            => this.Message = message;
    }
}
