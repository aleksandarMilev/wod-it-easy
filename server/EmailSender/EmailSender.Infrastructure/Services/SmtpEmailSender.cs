namespace WodItEasy.EmailSender.Infrastructure.Services
{
    using System.Net;
    using System.Net.Mail;
    using Microsoft.Extensions.Configuration;
    using Application.Features.EmailSend;

    public class SmtpEmailSender(IConfiguration configuration) : IEmailSender
    {
        private readonly IConfiguration config = configuration;

        public async Task SendAsync(
            string to,
            string subject,
            string body,
            bool isHtml)
        {
            var smtpConfig = this.config.GetSection("Smtp");
            
            var from = smtpConfig["From"];
            var host = smtpConfig["Host"];
            var port = int.Parse(smtpConfig["Port"] ?? "587");
            var username = smtpConfig["Username"];
            var password = smtpConfig["Password"];
            var enableSsl = bool.Parse(smtpConfig["EnableSsl"] ?? "true");

            using var smtpClient = new SmtpClient(host)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl
            };

            using var mailMessage = new MailMessage(
                from!,
                to,
                subject,
                body)
            {
                IsBodyHtml = isHtml
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
