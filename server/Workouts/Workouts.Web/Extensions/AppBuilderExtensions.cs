namespace WodItEasy.Workouts.Web.Extensions
{
    using Common.Application.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using static Common.Web.Constants;

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

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
            => app
                .UseSwagger()
                .UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "WodItEasy v1");
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