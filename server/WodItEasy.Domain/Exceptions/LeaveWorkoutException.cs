namespace WodItEasy.Domain.Exceptions
{
    using Common.Domain;

    public class LeaveWorkoutException : BaseDomainException
    {
        public LeaveWorkoutException() { }

        public LeaveWorkoutException(string message)
            => this.Message = message;
    }
}
