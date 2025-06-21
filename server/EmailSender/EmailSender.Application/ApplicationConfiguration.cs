namespace WodItEasy.EmailSender.Application
{
    using System.Reflection;
    using Common.Application;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        private static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();

        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddEventConsumers(ExecutingAssembly)
                .AddCommonApplication(
                    configuration,
                    ExecutingAssembly);
    }
}
