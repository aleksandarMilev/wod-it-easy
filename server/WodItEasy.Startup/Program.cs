using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using WodItEasy.Application;
using WodItEasy.Domain;
using WodItEasy.Infrastructure;
using WodItEasy.Startup;
using WodItEasy.Web;
using WodItEasy.Web.Extensions;
using WodItEasy.Web.Middlewares;

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