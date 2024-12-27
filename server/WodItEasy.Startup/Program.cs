namespace WodItEasy.Startup
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using WodItEasy.Application;
    using WodItEasy.Infrastructure;
    using WodItEasy.Web;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddApplication(builder.Configuration)
                .AddWebComponents();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .Initialize();

            app.Run();
        }
    }
}
