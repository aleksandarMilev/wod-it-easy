namespace WodItEasy.Application.Contracts
{
    using System.Threading.Tasks;

    public interface IRoleSeeder
    {
        Task SeedRole(
            string roleName,
            string email,
            string password);
    }
}
