namespace WodItEasy.Application.Features.Identity.Commands.Register
{
    using MediatR;
    using WodItEasy.Common.Application;

    public class RegisterCommand : IRequest<Result<RegisterOutputModel>>
    {
        public string Username { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public class RegisterCommandHandler(IIdentityService service)
            : IRequestHandler<RegisterCommand, Result<RegisterOutputModel>>
        {
            private readonly IIdentityService service = service;

            public async Task<Result<RegisterOutputModel>> Handle(
                RegisterCommand request, 
                CancellationToken cancellationToken)
                => await this.service.Register(
                    request.Username, 
                    request.Email,
                    request.Password);
        }
    }
}
