using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using WodItEasy.Workouts.Domain;
using WodItEasy.Workouts.Infrastructure;
using WodItEasy.Workouts.Startup;
using WodItEasy.Workouts.Web;
using WodItEasy.Workouts.Web.Extensions;
using WodItEasy.Workouts.Application;

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
    .UseAllowedCors()
    .UseAuthentication()
    .UseAuthorization()
    .UseAppEndpoints()
    .Initialize();
            
await app.SeedRoles(builder.Configuration);
await app.RunAsync();