namespace WodItEasy.Domain.Exceptions
{
    public class MembershipExpiredException : BaseDomainException
    {
        public MembershipExpiredException()
        {
        }

        public MembershipExpiredException(string message) => this.Message = message;
    }
}
