﻿namespace WodItEasy.Workouts.Domain
{
    using System.Reflection;
    using Common.Domain;
    using Microsoft.Extensions.DependencyInjection;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(
            this IServiceCollection services)
            => services
                .AddCommonDomain(
                    Assembly.GetExecutingAssembly());
    }
}
