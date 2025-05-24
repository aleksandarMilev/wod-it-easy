namespace WodItEasy.Workouts.Infrastructure.Identity.Jwt
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateJwtToken(
           string userId,
           string username,
           string email,
           bool rememberMe = false,
           bool isAdmin = false);
    }
}
