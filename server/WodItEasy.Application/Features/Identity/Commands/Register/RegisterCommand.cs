namespace WodItEasy.Application.Features.Identity.Commands.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class RegisterCommand : IRequest<Result<RegisterOutputModel>>
    {
        public string Username { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterOutputModel>>
        {
            private readonly IIdentityService service;

            public RegisterCommandHandler(IIdentityService service) 
                => this.service = service;

            public Task<Result<RegisterOutputModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
                => this.service.Register(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}
