namespace WodItEasy.EmailSender.Infrastructure
{
    using System.Reflection;
    using Application.Features.EmailSend;
    using Common.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Application.Features.EmailSend.Consumers;

    public static class InfrastructureConfiguration
    {
        private static readonly Type[] Consumers =
        {
            typeof(UserRegisteredConsumer),
        };

        public static async Task<IServiceCollection> AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services
                .AddCommonInfrastructure(
                    config,
                    Assembly.GetExecutingAssembly())
                .AddTransient<IEmailSender, EmailSender>()
                .Configure<EmailSettings>(
                    config.GetSection("MailSettings"));

            await services.AddEvents(
               config,
               usePolling: false,
               consumers: Consumers);

            return services;
        }
    }
}
