namespace WodItEasy.Application.Features.Identity.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using LoginUser;
    using MediatR;

    public class RegisterCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Username { get; private set; } = null!;

        public string Email { get; private set; } = null!;

        public string Password { get; private set; } = null!;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService service;

            public RegisterCommandHandler(IIdentityService service) 
                => this.service = service;

            public Task<Result<LoginOutputModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
                => this.service.Register(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}
