namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Athlete;
    using Domain.Models.Athletes;
    using Microsoft.EntityFrameworkCore;

    internal class AthleteRepository : DataRepository<Athlete>, IAthleteRepository
    {
        public AthleteRepository(WodItEasyDbContext data)
            : base(data) { }

        public async Task<bool> ExistsById(int id, CancellationToken cancellationToken = default)
            => await this
                .All()
                .AnyAsync(a => a.Id == id, cancellationToken);
    }
}
