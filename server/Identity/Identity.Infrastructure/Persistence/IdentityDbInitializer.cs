﻿namespace WodItEasy.Identity.Infrastructure.Persistence
{
    using Common.Infrastructure.Persistence;
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

        public override async Task Initialize()
        {
            await base.Initialize();
            await this.CreateAdmin();
        }

        private async Task CreateAdmin()
        {
            var existingRole = await this.roleManager.FindByNameAsync(AdminRoleName);

            if (existingRole is not null)
                return;

            var adminRole = new IdentityRole(AdminRoleName);
            await this.roleManager.CreateAsync(adminRole);

            var admin = new User()
            {
                UserName = AdminRoleName,
                Email = AdminEmail
            };

            await this.userManager.CreateAsync(admin, AdminPassword);
            await this.userManager.AddToRoleAsync(admin, AdminRoleName);
        }
    }
}
