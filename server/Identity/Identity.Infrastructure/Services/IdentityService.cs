namespace WodItEasy.Identity.Infrastructure.Services
{
    using Application;
    using Application.Commands.Login;
    using Application.Commands.Register;
    using Common.Application;
    using Common.Domain.Events;
    using Common.Domain.Models;
    using Infrastructure.Persistence;
    using JwtGenerator;
    using Microsoft.AspNetCore.Identity;

    using static Common.Domain.Constants;

    public class IdentityService(
        IdentityDbContext data,
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
        : IIdentityService
    {
        private const string InvalidLoginErrorMessage = "Invalid login attempt!";

        private readonly IdentityDbContext data = data;
        private readonly UserManager<User> userManager = userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator = jwtTokenGenerator;

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

                var @event = new UserRegisteredEvent(user.Email!, user.UserName!);

                this.data.Add(new Message(@event));
                await this.data.SaveChangesAsync();

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
                return InvalidLoginErrorMessage;

            var passwordIsValid = await this.userManager.CheckPasswordAsync(user, password);

            if (passwordIsValid)
            {
                var isAdmin = await this.userManager.IsInRoleAsync(user, AdminRoleName);

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
