﻿namespace WodItEasy.Application.Features.Athlete.Commands.Create
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

            public async Task<CreateAthleteOutputModel> Handle(
                CreateAthleteCommand request, 
                CancellationToken cancellationToken)
            {
                var userId = this.userService.UserId;
                var deletedAthlete = await this.repository.GetDeleted(userId!, cancellationToken);

                if (deletedAthlete is not null)
                {
                    deletedAthlete.Restore();
                    deletedAthlete.UpdateName(request.Name);

                    await this.repository.Save(deletedAthlete, cancellationToken);

                    return new CreateAthleteOutputModel() { Id = deletedAthlete.Id };
                }

                var athlete = this.factory
                    .WithName(request.Name)
                    .WithUserId(userId!)
                    .Build();

                await this.repository.Save(athlete, cancellationToken);

                return new CreateAthleteOutputModel() { Id = athlete.Id };
            }
        }
    }
}
