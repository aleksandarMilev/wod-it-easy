namespace WodItEasy.EmailSender.Domain.Models
{
    using Common.Domain.Models;
    using Exceptions;

    using static ModelConstants;

    public class Email : Entity<int>, IAggregateRoot
    {
        internal Email(
            string to,
            string subject,
            string body,
            bool isHtml)
        {
            this.Validate(to, subject, body);

            this.To = to;
            this.Subject = subject;
            this.Body = body;
            this.IsHtml = isHtml;
        }

        public string To { get; init; }

        public string Subject { get; init; }

        public string Body { get; init; }

        public bool IsHtml { get; init; }

        private void Validate(
            string to,
            string subject,
            string body)
        {
            this.ValidateTo(to);
            this.ValidateSubject(subject);
            this.ValidateBody(body);
        }

        private void ValidateTo(string to)
            => Guard.ForStringLength<InvalidEmailException>(
                to,
                ToMinNameLength,
                ToMaxNameLength,
                nameof(this.To));

        private void ValidateSubject(string subject)
            => Guard.ForStringLength<InvalidEmailException>(
                subject,
                SubjectMinNameLength,
                SubjectMaxNameLength,
                nameof(this.Subject));

        private void ValidateBody(string body)
            => Guard.ForStringLength<InvalidEmailException>(
                body,
                BodyMinNameLength,
                BodyMaxNameLength,
                nameof(this.Body));
    }
}
