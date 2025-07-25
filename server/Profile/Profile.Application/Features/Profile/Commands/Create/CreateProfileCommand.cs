﻿namespace WodItEasy.Profile.Application.Features.Profile.Commands.Create
{
    using Common;
    using Domain.Factories;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Contracts;

    public class CreateProfileCommand
        : ProfileCommand<CreateProfileCommand>, IRequest<Result<CreateProfileOutputModel>>
    {
        public class CreateProfileCommandHandler(
            ICurrentUserService userService,
            IProfileRepository repository,
            IProfileFactory factory)
            : IRequestHandler<CreateProfileCommand, Result<CreateProfileOutputModel>>
        {
            private readonly ICurrentUserService userService = userService;
            private readonly IProfileRepository repository = repository;
            private readonly IProfileFactory factory = factory;

            public async Task<Result<CreateProfileOutputModel>> Handle(
                CreateProfileCommand request,
                CancellationToken cancellationToken)
            {
                var profile = this.factory
                    .ForUser(this.userService.UserId!)
                    .WithAvatarUrl(request.AvatarUrl)
                    .WithBio(request.Bio)
                    .Build();

                await this.repository.Save(profile, cancellationToken);

                return new CreateProfileOutputModel() { Id = profile.Id };
            }
        }
    }
}
