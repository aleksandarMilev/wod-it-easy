namespace WodItEasy.EmailSender.Domain.Exceptions
{
    using Common.Domain;

    public class InvalidEmailException : BaseDomainException
    {
        public InvalidEmailException() { }

        public InvalidEmailException(string message)
            => this.Message = message;
    }
}
