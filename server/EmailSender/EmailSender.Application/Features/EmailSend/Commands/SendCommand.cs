namespace WodItEasy.EmailSender.Application.Features.EmailSend.Commands
{
    using Domain.Factories;
    using MediatR;

    public class SendCommand : IRequest
    {
        public string To { get; set; } = default!;

        public string Subject { get; set; } = default!;

        public string Body { get; set; } = default!;

        public bool IsHtml { get; set; }

        public class SendCommandHandler(
            IEmailSender sender,
            IEmailFactory factory) 
            : IRequestHandler<SendCommand>
        {
            private readonly IEmailSender sender = sender;
            private readonly IEmailFactory factory = factory;

            public async Task Handle(
                SendCommand request,
                CancellationToken cancellationToken)
            {
                var email = this.factory
                   .To(request.To)
                   .WithSubject(request.Subject)
                   .WithBody(request.Body)
                   .IsHtml(request.IsHtml)
                   .Build();

                await this.sender.SendAsync(
                    email.To,
                    email.Subject,
                    email.Body,
                    email.IsHtml);
            }
        }
    }
}
