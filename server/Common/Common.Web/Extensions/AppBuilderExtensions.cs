namespace WodItEasy.Common.Web.Extensions
{
    using HealthChecks.UI.Client;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Middlewares;

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> UseWebServices(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app
               .UseExceptionHandling(env)
               .UseValidationExceptionHandler()
               .UseHttpsRedirection()
               .UseRouting()
               .UseCors(options => options
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod())
               .UseSwaggerDocs(env)
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints
                   .MapHealthChecks()
                   .MapControllers());

            await app.Initialize();
            return app;
        }

        private static IApplicationBuilder UseExceptionHandling(
           this IApplicationBuilder app,
           IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            return app;
        }

        private static IApplicationBuilder UseSwaggerDocs(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
                app
                    .UseSwagger()
                    .UseSwaggerUI(config =>
                    {
                        config.SwaggerEndpoint("/swagger/v1/swagger.json", "WodItEasy v1");
                    });

            return app;
        }

        private static IEndpointRouteBuilder MapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return endpoints;
        }

        private static async Task<IApplicationBuilder> Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var initializers = serviceScope.ServiceProvider.GetServices<IDbInitializer>();

            foreach (var initializer in initializers)
                await initializer.Initialize();

            return app;
        }
    }
}
