namespace WodItEasy.Domain.Exceptions
{
    public class InvalidMembershipException : BaseDomainException
    {
        public InvalidMembershipException()
        {
        }

        public InvalidMembershipException(string message)
            => this.Message = message;
    }
}
