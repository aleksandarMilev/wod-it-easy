using WodItEasy.Common.Web.Extensions;
using WodItEasy.Identity.Application;
using WodItEasy.Identity.Infrastructure;
using WodItEasy.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebComponents();

var app = builder.Build();
app.UseWebServices(app.Environment);

await app.RunAsync();