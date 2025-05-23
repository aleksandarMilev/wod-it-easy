using Microsoft.AspNetCore.Builder;
using WodItEasy.Common.Web.Extensions;
using WodItEasy.Workouts.Application;
using WodItEasy.Workouts.Domain;
using WodItEasy.Workouts.Infrastructure;
using WodItEasy.Workouts.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebComponents();

var app = builder.Build();
app.UseWebServices(app.Environment);
            
await app.RunAsync();