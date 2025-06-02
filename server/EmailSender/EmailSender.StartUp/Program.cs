using WodItEasy.Common.Web.Extensions;
using WodItEasy.EmailSender.Application;
using WodItEasy.EmailSender.Domain;
using WodItEasy.EmailSender.Infrastructure;
using WodItEasy.EmailSender.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebComponents(builder.Environment);

var app = builder.Build();
await app.UseWebServices(app.Environment);
await app.RunAsync();