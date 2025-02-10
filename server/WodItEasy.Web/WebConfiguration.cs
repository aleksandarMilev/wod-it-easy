namespace WodItEasy.Web
{
    using Application.Common;
    using Application.Contracts;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Service;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(config => {
                    var apiInfo = new OpenApiInfo() 
                    {
                        Title = "WodItEasy API", 
                        Version = "v1" 
                    };

                    config.SwaggerDoc("v1", apiInfo);
                })
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddValidatorsFromAssemblyContaining<Result>()
                .AddControllers()
                .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
