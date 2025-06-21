namespace WodItEasy.EmailSender.Infrastructure.Services
{
    using System.Net;
    using System.Net.Mail;
    using Application.Features.EmailSend;
    using Microsoft.Extensions.Options;

    public class EmailSender(
        IOptions<EmailSettings> settings) : IEmailSender
    {
        private readonly EmailSettings settings = settings.Value;

        public async Task Send(
            string to,
            string subject,
            string body,
            bool isHtml)
        {
            using var client = new SmtpClient(
                this.settings.Host,
                this.settings.Port)
            {
                Credentials = new NetworkCredential(
                    this.settings.Mail,
                    this.settings.Password),

                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(
                    this.settings.Mail,
                    this.settings.DisplayName),

                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mail.To.Add(to);

            await client.SendMailAsync(mail);
        }
    }
}
