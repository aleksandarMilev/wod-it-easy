namespace WodItEasy.Startup
{
    using System.Threading.Tasks;
    using Application;
    using Domain;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Web;
    using Web.Extensions;
    using Web.Middlewares;

    public class Program
    {
        public static async Task Main(string[] args)
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
                app
                    .UseSwaggerDocs()
                    .UseDeveloperExceptionPage();
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
            
            await app.SeedRoles(builder.Configuration);

            await app.RunAsync();
        }
    }
}
