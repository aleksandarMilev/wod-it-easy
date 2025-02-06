namespace WodItEasy.Application.Features.Identity.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Commands.Common;
    using MediatR;

    public class LoginCommand : IRequest<Result<IdentityOutputModel>>
    {
        public string Credentials { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<IdentityOutputModel>>
        {
            private readonly IIdentityService service;

            public LoginCommandHandler(IIdentityService service) 
                => this.service = service;

            public async Task<Result<IdentityOutputModel>> Handle(LoginCommand request, CancellationToken cancellationToken) 
                => await this.service.Login(
                    request.Credentials,
                    request.Password,
                    request.RememberMe);
        }
    }
}
