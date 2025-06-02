namespace WodItEasy.Profile.Infrastructure.Repositories
{
    using Application.Features.Profile;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using WodItEasy.Common.Application.Commands;
    using WodItEasy.Profile.Application.Features.Profile.Queries.Details;

    internal class ProfileRepository(
        ProfileDbContext data,
        IMapper mapper)
        : DataRepository<ProfileDbContext, Domain.Models.Profile.Profile>(data),
          IProfileRepository
    {
        private readonly IMapper mapper = mapper;

        public async Task<Domain.Models.Profile.Profile?> ById(
            string userId,
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

        public async Task<ProfileDetailsOutputModel?> Details(
            string userId,
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .Where(p => p.UserId == userId)
                .ProjectTo<ProfileDetailsOutputModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<bool> Delete(
            string userId,
            CancellationToken cancellationToken = default)
        {
            var profile = await this.ById(userId, cancellationToken);

            if (profile is null)
                return false;

            this.Data.Remove(profile);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
