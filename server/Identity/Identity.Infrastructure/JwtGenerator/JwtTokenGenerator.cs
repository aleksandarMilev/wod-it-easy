namespace WodItEasy.Identity.Infrastructure.JwtGenerator
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Common.Application.Settings;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using static Common.Domain.Constants;

    public class JwtTokenGenerator(
        IOptions<ApplicationSettings> applicationSettings)
        : IJwtTokenGenerator
    {
        private const int DefaultTokenExpirationTimeDays = 7;
        private const int ExtendedTokenExpirationTimeDays = 30;

        private readonly ApplicationSettings applicationSettings = applicationSettings.Value;

        public string GenerateJwtToken(
            string userId,
            string username,
            string email,
            bool rememberMe = false,
            bool isAdmin = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(applicationSettings.Secret);

            var claimList = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Email, email)
            };

            if (isAdmin)
            {
                claimList.Add(new(ClaimTypes.Role, AdminRoleName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = rememberMe
                    ? DateTime.UtcNow.AddDays(ExtendedTokenExpirationTimeDays)
                    : DateTime.UtcNow.AddDays(DefaultTokenExpirationTimeDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
