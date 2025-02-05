namespace WodItEasy.Application.Features.Identity.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class LoginCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Credentials { get; private set; } = null!;

        public string Password { get; private set; } = null!;

        public bool RememberMe { get; private set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService service;

            public LoginCommandHandler(IIdentityService service) 
                => this.service = service;

            public async Task<Result<LoginOutputModel>> Handle(LoginCommand request, CancellationToken cancellationToken) 
                => await this.service.Login(
                    request.Credentials,
                    request.Password,
                    request.RememberMe);
        }
    }
}
