﻿namespace WodItEasy.Application.Features.Identity.Commands.Login
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class LoginCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Credentials { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService service;

            public LoginCommandHandler(IIdentityService service) 
                => this.service = service;

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
