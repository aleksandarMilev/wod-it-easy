namespace WodItEasy.EmailSender.Domain.Factories
{
    using Common.Domain;
    using Models;

    public interface IEmailFactory : IFactory<Email>
    {
        IEmailFactory To(string to);

        IEmailFactory WithSubject(string subject);

        IEmailFactory WithBody(string body);

        IEmailFactory IsHtml(bool isHtml);
    }
}
