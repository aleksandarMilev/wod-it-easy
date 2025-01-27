namespace WodItEasy.Application.Features.Athlete.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Domain.Factories.Athlete;
    using MediatR;

    public class CreateAthleteCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;

        public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, int>
        {
            private readonly IAthleteFactory factory;
            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public CreateAthleteCommandHandler(
                IAthleteFactory factory,
                IAthleteRepository repository,
                ICurrentUserService userService)
            {
                this.factory = factory;
                this.userService = userService;
                this.repository = repository;
            }

            public async Task<int> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
            {
                var athlete = factory
                    .WithName(request.Name)
                    .WithUserId(userService.UserId!)
                    .Build();

                await repository.Save(athlete, cancellationToken);

                return athlete.Id;
            }
        }
    }
}
