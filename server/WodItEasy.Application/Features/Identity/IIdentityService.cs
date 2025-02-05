namespace WodItEasy.Application.Features.Identity
{
    using System.Threading.Tasks;
    using Application.Common;
    using Commands.Common;

    public interface IIdentityService
    {
        Task<Result<IdentityOutputModel>> Register(
            string username, 
            string email, 
            string password);

        Task<Result<IdentityOutputModel>> Login(
            string credentials, 
            string password, 
            bool rememberMe);
    }
}
