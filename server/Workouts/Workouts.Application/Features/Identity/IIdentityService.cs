namespace WodItEasy.Workouts.Application.Features.Identity
{
    using Commands.Login;
    using Commands.Register;
    using Common.Application;

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
