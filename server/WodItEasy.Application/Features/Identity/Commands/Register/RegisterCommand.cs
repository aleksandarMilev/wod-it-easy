namespace WodItEasy.Application.Features.Identity.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using LoginUser;
    using MediatR;

    public class RegisterCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Username { get; } = null!;

        public string Email { get; } = null!;

        public string Password { get; } = null!;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService identityService;

            public RegisterCommandHandler(IIdentityService identity) 
                => this.identityService = identity;

            public Task<Result<LoginOutputModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
                => this.identityService.Register(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}
