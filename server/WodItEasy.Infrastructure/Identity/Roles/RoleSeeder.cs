namespace WodItEasy.Infrastructure.Identity.Roles
{
    using System.Threading.Tasks;
    using Application.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class RoleSeeder : IRoleSeeder
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleSeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedRole(
            string roleName,
            string email,
            string password)
        {
            if (await this.roleManager.RoleExistsAsync(roleName))
            {
                return;
            }

            var role = new IdentityRole(roleName);
            await this.roleManager.CreateAsync(role);

            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new User()
                {
                    Email = email,
                    UserName = email
                };

                await this.userManager.CreateAsync(user, password);
            }

            if (await this.userManager.IsInRoleAsync(user, roleName))
            {
                return;
            }

            await this.userManager.AddToRoleAsync(user, roleName);
        }
    }
}
