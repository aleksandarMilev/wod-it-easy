namespace WodItEasy.Identity.Infrastructure.Persistence
{
    using Common.Infrastructure;
    using Microsoft.AspNetCore.Identity;

    using static Common.Domain.Constants;

    internal class IdentityDbInitializer(
        IdentityDbContext data,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        : DbInitializer(data)
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;

        public override void Initialize()
        {
            base.Initialize();
            this.SeedAdministrator();
        }

        private void SeedAdministrator()
            => Task
                .Run(async () =>
                {
                    var existingRole = await this.roleManager.FindByNameAsync(AdminRoleName);

                    if (existingRole is not null)
                        return;

                    var adminRole = new IdentityRole();

                    await this.roleManager.CreateAsync(adminRole);

                    var admin = new User()
                    {
                        UserName = AdminRoleName,
                        Email = AdminEmail
                    };

                    await this.userManager.CreateAsync(admin, AdminPassword);
                    await this.userManager.AddToRoleAsync(admin, AdminRoleName);
                })
                .GetAwaiter()
                .GetResult();
    }
}
