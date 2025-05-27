namespace WodItEasy.Profile.Infrastructure.Repositories
{
    using Application.Features.Profile;
    using AutoMapper;
    using Common.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    internal class ProfileRepository(
        ProfileDbContext data,
        IMapper mapper)
        : DataRepository<ProfileDbContext, Domain.Models.Profile.Profile>(data),
          IProfileRepository
    {
        private readonly IMapper mapper = mapper;

        public async Task<Domain.Models.Profile.Profile?> ById(
            int id,
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<bool> Delete(
           int id,
           CancellationToken cancellationToken = default)
        {
            var profile = await this.ById(id, cancellationToken);

            if (profile is null)
            {
                return false;
            }

            this.Data.Remove(profile);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
