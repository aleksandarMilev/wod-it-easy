namespace WodItEasy.Application.Features.Athlete.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using Domain.Factories.Athlete;
    using MediatR;

    public class CreateAthleteCommand : AthleteCommand<CreateAthleteCommand>, IRequest<CreateAthleteOutputModel>
    {
        public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, CreateAthleteOutputModel>
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
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<CreateAthleteOutputModel> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
            {
                var athlete = this.factory
                    .WithName(request.Name)
                    .WithUserId(userService.UserId!)
                    .Build();

                await this.repository.Save(athlete, cancellationToken);

                return new CreateAthleteOutputModel(athlete.Id);
            }
        }
    }
}
