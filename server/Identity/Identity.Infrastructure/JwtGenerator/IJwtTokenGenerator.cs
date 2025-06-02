namespace WodItEasy.Identity.Infrastructure.JwtGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(
           string userId,
           string username,
           string email,
           bool rememberMe = false,
           bool isAdmin = false);
    }
}
