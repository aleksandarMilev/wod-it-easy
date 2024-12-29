namespace WodItEasy.Web
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwagger()
                .AddControllers()
                .AddNewtonsoftJson();

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var apiInfo = new OpenApiInfo()
            {
                Title = "WodItEasy API",
                Version = "v1",

            };

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", apiInfo));

            return services;
        }
    }
}
