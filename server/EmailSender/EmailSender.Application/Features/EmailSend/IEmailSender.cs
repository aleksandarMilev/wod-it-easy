namespace WodItEasy.EmailSender.Application.Features.EmailSend
{
    public interface IEmailSender
    {
        Task Send(
            string to,
            string subject,
            string body,
            bool isHtml);
    }
}
