namespace WodItEasy.Identity.Infrastructure.Persistence
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    internal class IdentityDbContext(
        DbContextOptions<IdentityDbContext> options)
        : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}