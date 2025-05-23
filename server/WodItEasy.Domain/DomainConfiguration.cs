namespace WodItEasy.Domain
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using WodItEasy.Common.Domain;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(
            this IServiceCollection services)
            => services
                .AddCommonDomain(
                    Assembly.GetExecutingAssembly());
    }
}
