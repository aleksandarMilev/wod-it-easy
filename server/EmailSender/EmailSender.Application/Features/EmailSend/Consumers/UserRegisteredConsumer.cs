namespace WodItEasy.EmailSender.Application.Features.EmailSend.Consumers
{
    using System.Net;
    using Commands;
    using Common.Domain.Events;
    using MassTransit;
    using MediatR;

    public class UserRegisteredConsumer(
        IMediator mediator)
        : IConsumer<UserRegisteredEvent>
    {
        private readonly IMediator mediator = mediator;

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var message = context.Message;
            var htmlBody = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  <style>
    @import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600&display=swap');
    
    body {{
      margin: 0;
      padding: 0;
      background-color: #f8fafc;
      font-family: 'Inter', -apple-system, BlinkMacSystemFont, sans-serif;
      line-height: 1.5;
      color: #334155;
    }}
    .wrapper {{
      width: 100%;
      table-layout: fixed;
      padding: 40px 0;
      background-color: #f8fafc;
    }}
    .card {{
      background-color: #ffffff;
      max-width: 600px;
      margin: 0 auto;
      border-radius: 12px;
      overflow: hidden;
      box-shadow: 0 4px 24px rgba(0, 0, 0, 0.05);
      border: 1px solid #e2e8f0;
    }}
    .header {{
      background: linear-gradient(135deg, #3b82f6, #6366f1);
      padding: 48px 32px;
      text-align: center;
      color: #ffffff;
    }}
    .header h1 {{
      margin: 0;
      font-size: 28px;
      font-weight: 600;
      letter-spacing: -0.5px;
    }}
    .content {{
      padding: 40px 32px;
      font-size: 16px;
    }}
    .content p {{
      margin: 0 0 20px 0;
    }}
    .highlight {{
      color: #3b82f6;
      font-weight: 500;
    }}
    .divider {{
      border: none;
      border-top: 1px solid #f1f5f9;
      margin: 0 32px;
    }}
    .footer {{
      padding: 24px 32px;
      text-align: center;
      font-size: 14px;
      color: #64748b;
    }}
    .footer a {{
      color: #3b82f6;
      text-decoration: none;
      font-weight: 500;
      transition: all 0.2s ease;
    }}
    .footer a:hover {{
      color: #2563eb;
      text-decoration: underline;
    }}
    .button {{
      display: inline-block;
      background-color: #3b82f6;
      color: white !important;
      text-decoration: none;
      padding: 12px 24px;
      border-radius: 8px;
      font-weight: 500;
      margin: 16px 0;
      transition: all 0.2s ease;
    }}
    .button:hover {{
      background-color: #2563eb;
      transform: translateY(-1px);
      box-shadow: 0 4px 12px rgba(59, 130, 246, 0.2);
    }}
    @media screen and (max-width: 600px) {{
      .card {{
        border-radius: 0;
        width: 100% !important;
        border-left: none;
        border-right: none;
      }}
      .content, .header, .footer {{
        padding-left: 24px;
        padding-right: 24px;
      }}
      .header {{
        padding-top: 40px;
        padding-bottom: 40px;
      }}
    }}
  </style>
</head>
<body>
  <div class=""wrapper"">
    <div class=""card"">
      <div class=""header"">
        <h1>Welcome to WodItEasy!</h1>
      </div>
      <div class=""content"">
        <p>Hi <span class=""highlight"">{WebUtility.HtmlEncode(message.Name)}</span>,</p>
        <p>Thanks for registering at <strong>WodItEasy</strong>. We're thrilled to have you join our community!</p>
        <p>You're now part of a growing group of CrossFit enthusiasts who are making their workouts easier and more effective.</p>
        <p>If you have any questions or need assistance, don't hesitate to reply to this email—we're always happy to help.</p>
        <p>Happy training!</p>
        <p>Best regards,<br/>The <strong>WodItEasy</strong> Team</p>
      </div>
      <hr class=""divider""/>
      <div class=""footer"">
        <p>WodItEasy, <a href=""https://github.com/aleksandarMilev/wod-it-easy"">open source project</a></p>
      </div>
    </div>
  </div>
</body>
</html>";

            var command = new SendCommand
            {
                To = message.Email,
                Subject = "Welcome to WodItEasy!",
                Body = htmlBody,
                IsHtml = true
            };

            await this.mediator.Send(command);
        }
    }
}
