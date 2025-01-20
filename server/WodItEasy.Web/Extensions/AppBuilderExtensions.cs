namespace WodItEasy.Web.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Application.Contracts;
    using Microsoft.Extensions.Configuration;

    using static Common.Constants;

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseAppEndpoints(this IApplicationBuilder app) 
            => app.UseEndpoints(e => e.MapControllers());

        public static IApplicationBuilder UseAllowedCors(this IApplicationBuilder app)
            => app
                 .UseCors(opt =>
                 {
                     opt.AllowAnyOrigin();
                     opt.AllowAnyHeader();
                     opt.AllowAnyMethod();
                 });

        public static async Task<IApplicationBuilder> SeedRoles(
            this IApplicationBuilder app,
            IConfiguration configuration)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeeder>();

            await roleSeeder.SeedRole(
                AdminRoleName,
                configuration.GetAdminEmail(),
                configuration.GetAdminPassword());

            return app;
        }
    }
}