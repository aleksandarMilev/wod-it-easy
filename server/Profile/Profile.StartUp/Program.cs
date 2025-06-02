
using WodItEasy.Common.Web.Extensions;
using WodItEasy.Profile.Application;
using WodItEasy.Profile.Domain;
using WodItEasy.Profile.Infrastructure;
using WodItEasy.Profile.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebComponents(builder.Environment);

var app = builder.Build();
await app.UseWebServices(app.Environment);
await app.RunAsync();