namespace WodItEasy.Common.Domain
{
    public abstract class BaseDomainException : Exception
    {
        private string? message;

        public new string Message
        {
            get => message ?? base.Message;
            set => message = value;
        }
    }
}
