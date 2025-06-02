namespace WodItEasy.EmailSender.Infrastructure
{
    using System.Reflection;
    using Application.Features.EmailSend;
    using Common.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using Services;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCommonInfrastructure<EmailSenderDbContext>(
                    configuration,
                    Assembly.GetExecutingAssembly())
            .AddTransient<IDbInitializer, EmailSenderDbInitializer>()
            .AddTransient<IEmailSender, SmtpEmailSender>();;
    }
}
