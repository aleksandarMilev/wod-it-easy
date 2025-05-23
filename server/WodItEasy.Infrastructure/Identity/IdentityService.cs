namespace WodItEasy.Infrastructure.Identity
{
    using Application.Features.Identity;
    using Application.Features.Identity.Commands.Login;
    using Application.Features.Identity.Commands.Register;
    using Jwt;
    using Microsoft.AspNetCore.Identity;
    using WodItEasy.Common.Application;

    using static Constants;

    public class IdentityService(
        UserManager<User> userManager,
        IJwtTokenGeneratorService jwtTokenGenerator)
        : IIdentityService
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly IJwtTokenGeneratorService jwtTokenGenerator = jwtTokenGenerator;

        public async Task<Result<RegisterOutputModel>> Register(
            string username,
            string email,
            string password)
        {
            var user = new User()
            {
                UserName = username,
                Email = email
            };

            var identityResult = await this.userManager.CreateAsync(user, password);

            if (identityResult.Succeeded)
            {
                var token = this.jwtTokenGenerator.GenerateJwtToken(
                    user.Id,
                    user.UserName,
                    user.Email!);

                return new RegisterOutputModel() { Token = token };
            }

            return string.Join("; ", identityResult.Errors.Select(e => e.Description));
        }

        public async Task<Result<LoginOutputModel>> Login(
            string credentials,
            string password,
            bool rememberMe)
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

                return new LoginOutputModel() { Token = token };
            }

            return InvalidLoginErrorMessage;
        }
    }
}
