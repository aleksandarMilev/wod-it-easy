namespace WodItEasy.Application.Features.Identity.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Commands.Common;
    using MediatR;

    public class RegisterCommand : IRequest<Result<IdentityOutputModel>>
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<IdentityOutputModel>>
        {
            private readonly IIdentityService service;

            public RegisterCommandHandler(IIdentityService service) 
                => this.service = service;

            public Task<Result<IdentityOutputModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
                => this.service.Register(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}
