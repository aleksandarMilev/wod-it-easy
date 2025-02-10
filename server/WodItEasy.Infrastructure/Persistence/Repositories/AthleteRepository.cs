namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Athlete;
    using Application.Features.Athlete.Queries.Details;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Domain.Models.Athletes;
    using Microsoft.EntityFrameworkCore;

    internal class AthleteRepository : DataRepository<Athlete>, IAthleteRepository
    {
        private readonly IMapper mapper;

        public AthleteRepository(WodItEasyDbContext data, IMapper mapper)
            : base(data)
                => this.mapper = mapper;

        public async Task<Athlete?> ByUserId(
            string userId, 
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);

        public async Task<Athlete?> GetDeleted(
            string userId, 
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);

        public async Task<Athlete?> ById(
            int id, 
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public async Task<GetAthleteDetailsOutputModel?> GetOutputModel(
            string userId, 
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .Where(a => a.UserId == userId)
                .ProjectTo<GetAthleteDetailsOutputModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<int?> GetId(
            string userId, 
            CancellationToken cancellationToken = default)
        {
            var id = await this
                .All()
                .Where(a => a.UserId == userId)
                .Select(a => a.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return id == 0 ? null : id;
        }

        public async Task<bool> ExistsById(
            int id, 
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .AnyAsync(a => a.Id == id, cancellationToken);

        public async Task<bool> Delete(
            string userId, 
            CancellationToken cancellationToken = default)
        {
            var athlete = await this.ByUserId(userId, cancellationToken);

            if (athlete is null)
            {
                return false;
            }

            this.Data.Remove(athlete);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
