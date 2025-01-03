namespace WodItEasy.Application.Features.Identity.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using LoginUser;
    using MediatR;

    public class RegisterCommand : IRequest<Result<LoginOutputModel>>
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService identityService;

            public RegisterCommandHandler(IIdentityService identity) => this.identityService = identity;

            public Task<Result<LoginOutputModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
                => this.identityService.RegisterAsync(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}
