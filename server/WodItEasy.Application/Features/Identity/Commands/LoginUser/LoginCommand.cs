﻿namespace WodItEasy.Application.Features.Identity.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class LoginCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Credentials { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService identityService;

            public LoginCommandHandler(IIdentityService identity) => this.identityService = identity;

            public async Task<Result<LoginOutputModel>> Handle(LoginCommand request, CancellationToken cancellationToken) 
                => await this.identityService.LoginAsync(
                    request.Credentials,
                    request.Password,
                    request.RememberMe);
        }
    }
}
