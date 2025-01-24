namespace WodItEasy.Application.Features.Athlete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Domain.Models.Athletes;

    public interface IAthleteRepository : IRepository<Athlete>
    {
        Task<bool> ExistsById(int id, CancellationToken cancellationToken = default);
    }
}
