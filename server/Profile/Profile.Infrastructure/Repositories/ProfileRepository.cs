namespace WodItEasy.Profile.Infrastructure.Repositories
{
    using Application.Features.Profile;
    using AutoMapper;
    using Common.Infrastructure;
    using Persistence;

    internal class ProfileRepository(
        ProfileDbContext data,
        IMapper mapper)
        : DataRepository<ProfileDbContext, Domain.Models.Profile.Profile>(data),
          IProfileRepository
    {
        private readonly IMapper mapper = mapper;
    }
}
