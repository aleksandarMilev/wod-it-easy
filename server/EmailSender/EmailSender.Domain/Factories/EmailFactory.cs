namespace WodItEasy.EmailSender.Domain.Factories
{
    using Models;

    public class EmailFactory : IEmailFactory
    {
        private string to = default!;
        private string subject = default!;
        private string body = default!;
        private bool isHtml;

        public IEmailFactory To(string to)
        {
            this.to = to;

            return this;
        }

        public IEmailFactory WithSubject(string subject)
        {
            this.subject = subject;

            return this;
        }
      
        public IEmailFactory WithBody(string body)
        {
            this.body = body;

            return this;
        }

        public IEmailFactory IsHtml(bool isHtml)
        {
            this.isHtml = isHtml;

            return this;
        }

        public Email Build()
            => new(
                this.to,
                this.subject,
                this.body,
                this.isHtml);

    }
}
