namespace WodItEasy.Identity.Application.Commands.Login
{
    using MediatR;
    using WodItEasy.Common.Application;

    public class LoginCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Credentials { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; }

        public class LoginCommandHandler(IIdentityService service)
            : IRequestHandler<LoginCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService service = service;

            public async Task<Result<LoginOutputModel>> Handle(
                LoginCommand request, 
                CancellationToken cancellationToken) 
                => await this.service.Login(
                    request.Credentials,
                    request.Password,
                    request.RememberMe);
        }
    }
}
