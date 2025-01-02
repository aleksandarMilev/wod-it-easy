namespace WodItEasy.Infrastructure.Identity
{
    public interface IJwtTokenGeneratorSerivce
    {
        string GenerateJwtToken(
           string userId,
           string username,
           string email,
           bool rememberMe = false,
           bool isAdmin = false);
    }
}
