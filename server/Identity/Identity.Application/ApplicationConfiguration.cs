﻿namespace WodItEasy.Identity.Application
{
    using System.Reflection;
    using Common.Application;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCommonApplication(
                    configuration,
                    Assembly.GetExecutingAssembly());
    }
}
