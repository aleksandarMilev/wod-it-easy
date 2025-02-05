namespace WodItEasy.Infrastructure.Identity
{
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Identity;
    using Application.Features.Identity.Commands.Common;
    using Jwt;
    using Microsoft.AspNetCore.Identity;

    using static Constants;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly IJwtTokenGeneratorService jwtTokenGenerator;

        public IdentityService(UserManager<User> userManager, IJwtTokenGeneratorService jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<IdentityOutputModel>> Register(string username, string email, string password)
        {
            var user = new User()
            {
                UserName = username,
                Email = email
            };

            var identityResult = await this.userManager.CreateAsync(user, password);

            if (identityResult.Succeeded)
            {
                var token = this.jwtTokenGenerator.GenerateJwtToken(user.Id, user.UserName, user.Email!);

                return new IdentityOutputModel(token);
            }

            return string.Join("; ", identityResult.Errors.Select(e => e.Description));
        }

        public async Task<Result<IdentityOutputModel>> Login(string credentials, string password, bool rememberMe)
        {
            var user = await this.userManager.FindByNameAsync(credentials);
            user ??= await this.userManager.FindByEmailAsync(credentials);

            if (user is null)
            {
                return InvalidLoginErrorMessage;
            }

            var passwordIsValid = await this.userManager.CheckPasswordAsync(user, password);

            if (passwordIsValid)
            {
                var isAdmin = await this.userManager.IsInRoleAsync(user, AdministratorRoleName);

                var token = this.jwtTokenGenerator.GenerateJwtToken(
                    user.Id,
                    user.UserName!,
                    user.Email!,
                    rememberMe,
                    isAdmin);

                return new IdentityOutputModel(token);
            }

            return InvalidLoginErrorMessage;
        }
    }
}
