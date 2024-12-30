namespace WodItEasy.Startup
{
    using Application;
    using Domain;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Web;
    using Web.Middleware;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddDomain()
                .AddApplication(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddWebComponents();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseValidationExceptionHandler()
                .UseAllowedCors()
                .UseAuthentication()
                .UseAuthorization()
                .UseAppEndpoints()
                .Initialize();

            app.Run();
        }
    }
}
