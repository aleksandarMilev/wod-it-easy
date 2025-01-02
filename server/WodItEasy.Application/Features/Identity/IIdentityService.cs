namespace WodItEasy.Application.Features.Identity
{
    using System.Threading.Tasks;
    using Application.Common;
    using Commands.LoginUser;

    public interface IIdentityService
    {
        Task<Result<LoginOutputModel>> RegisterAsync(string username, string email, string password);

        Task<Result<LoginOutputModel>> LoginAsync(string credentials, string password, bool rememberMe);
    }
}
