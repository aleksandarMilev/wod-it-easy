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

        public class CreateUserCommandHandler : IRequestHandler<RegisterCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService identity;

            public CreateUserCommandHandler(IIdentityService identity) => this.identity = identity;

            public Task<Result<LoginOutputModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
                => this.identity.RegisterAsync(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}