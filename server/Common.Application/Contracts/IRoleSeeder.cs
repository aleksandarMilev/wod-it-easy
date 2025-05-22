namespace WodItEasy.Common.Application.Contracts
{
    public interface IRoleSeeder
    {
        Task SeedRole(
            string roleName,
            string email,
            string password);
    }
}
