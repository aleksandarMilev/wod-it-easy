namespace WodItEasy.Domain
{
    using Factories;
    using Microsoft.Extensions.DependencyInjection;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .Scan(scan =>
                {
                    scan
                        .FromCallingAssembly()
                        .AddClasses(c => c.AssignableTo(typeof(IFactory<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime();
                });
    }
}
