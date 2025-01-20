namespace WodItEasy.Application.Features.Identity
{
    using System.Threading.Tasks;
    using Commands.LoginUser;
    using Common;

    public interface IIdentityService
    {
        Task<Result<LoginOutputModel>> Register(
            string username, 
            string email, 
            string password);

        Task<Result<LoginOutputModel>> Login(
            string credentials, 
            string password, 
            bool rememberMe);
    }
}
