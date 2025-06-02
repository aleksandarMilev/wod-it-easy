namespace WodItEasy.EmailSender.Application.Features.EmailSend
{
    public interface IEmailSender
    {
        Task SendAsync(
            string to,
            string subject,
            string body,
            bool isHtml);
    }
}
