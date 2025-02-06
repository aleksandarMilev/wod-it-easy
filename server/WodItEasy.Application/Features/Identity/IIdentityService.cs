namespace WodItEasy.Application.Features.Identity
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Identity.Commands.Login;
    using Application.Features.Identity.Commands.Register;

    public interface IIdentityService
    {
        Task<Result<RegisterOutputModel>> Register(
            string username, 
            string email, 
            string password);

        Task<Result<LoginOutputModel>> Login(
            string credentials, 
            string password, 
            bool rememberMe);
    }
}
