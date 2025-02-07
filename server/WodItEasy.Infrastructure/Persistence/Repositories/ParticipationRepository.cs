namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Participations;
    using Application.Features.Participations.Queries.Mine;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Domain.Models.Participation;
    using Microsoft.EntityFrameworkCore;
   
    internal class ParticipationRepository : DataRepository<Participation>, IParticipationRepository
    {
        private readonly IMapper mapper;

        public ParticipationRepository(WodItEasyDbContext data, IMapper mapper)
            : base(data)
                => this.mapper = mapper;

        public async Task<Participation?> ById(
            int id, 
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<bool> Exists(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken)
                => await this
                    .All()
                    .AsNoTracking()
                    .AnyAsync(
                        p => p.AthleteId == athleteId && p.WorkoutId == workoutId,
                        cancellationToken);

        public async Task<int> GetId(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken)
                => await this
                    .All()
                    .AsNoTracking()
                    .Where(p => p.AthleteId == athleteId && p.WorkoutId == workoutId)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync(cancellationToken);

        public async Task<PaginatedOutputModel<MyParticipationsOutputModel>> Mine(
            int athleteId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = this
                .All()
                .AsNoTracking()
                .Where(p => p.AthleteId == athleteId)
                .OrderByDescending(p => p.JoinedAt)
                .ProjectTo<MyParticipationsOutputModel>(this.mapper.ConfigurationProvider);

            var total = query.Count();

            var participations = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedOutputModel<MyParticipationsOutputModel>(
                participations,
                total,
                pageIndex,
                pageSize);
        }

        public async Task<bool> Delete(
            int id, 
            CancellationToken cancellationToken)
        {
            var participation = await this
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (participation is null)
            {
                return false;
            }

            this.Data.Remove(participation);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
