namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Athlete;
    using Domain.Models.Athletes;
    using Microsoft.EntityFrameworkCore;

    internal class AthleteRepository : DataRepository<Athlete>, IAthleteRepository
    {
        public AthleteRepository(WodItEasyDbContext data)
            : base(data) { }

        public async Task<int?> GetId(string userId, CancellationToken cancellationToken = default)
        {
            var id = await this
                .All()
                .Where(a => a.UserId == userId)
                .Select(a => a.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return id == 0 ? null : id;
        }

        public async Task<bool> ExistsById(int id, CancellationToken cancellationToken = default)
            => await this
                .All()
                .AnyAsync(a => a.Id == id, cancellationToken);
    }
}
