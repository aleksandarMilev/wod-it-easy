using WodItEasy.Common.Web.Extensions;
using WodItEasy.Identity.Application;
using WodItEasy.Identity.Infrastructure;
using WodItEasy.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication(builder.Configuration);
await builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebComponents(builder.Environment);

var app = builder.Build();
await app.UseWebServices(app.Environment);
await app.RunAsync();