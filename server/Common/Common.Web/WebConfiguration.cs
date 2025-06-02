namespace WodItEasy.Common.Web
{
    using Application;
    using Application.Contracts;
    using FluentValidation;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Service;

    public static class WebConfiguration
    {
        public static IServiceCollection AddCommonWebComponents(
            this IServiceCollection services,
            IWebHostEnvironment env)
        {
            services
                .AddEndpointsApiExplorer()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddValidatorsFromAssemblyContaining<Result>()
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddHealthChecks();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            if (env.IsDevelopment())
            {
                services.AddSwaggerGen(config =>
                {
                    var apiInfo = new OpenApiInfo()
                    {
                        Title = "WodItEasy API",
                        Version = "v1"
                    };

                    config.SwaggerDoc("v1", apiInfo);
                });
            }
                
            return services;
        }
    }
}
