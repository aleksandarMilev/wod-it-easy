namespace WodItEasy.Common.Infrastructure.Services
{
    using Domain.Models;
    using Events;
    using Hangfire;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal class MessagesHostedService(
        IEventPublisher eventPublisher,
        IRecurringJobManager recurringJob,
        IServiceScopeFactory serviceScopeFactory) : IHostedService
    {
        private const string CronExpression = "*/5 * * * * *";

        private readonly IEventPublisher eventPublisher = eventPublisher;
        private readonly IRecurringJobManager recurringJob = recurringJob;
        private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = this.serviceScopeFactory.CreateScope();
            var data = scope.ServiceProvider.GetService<DbContext>();

            if (!await data!.Database.CanConnectAsync(cancellationToken))
                await data.Database.MigrateAsync(cancellationToken);

            this.recurringJob.AddOrUpdate(
                nameof(MessagesHostedService),
                () => this.ProcessPendingMessages(),
                CronExpression);
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public async Task ProcessPendingMessages()
        {
            using var scope = this.serviceScopeFactory.CreateScope();
            var data = scope.ServiceProvider.GetService<DbContext>();

            var messages = await data!
                .Set<Message>()
                .Where(m => !m.Published)
                .OrderBy(m => m.Id)
                .ToListAsync();

            foreach (var message in messages)
            {
                await this.eventPublisher.Publish(message.Data, message.Type);

                message.MarkAsPublished();
                data.SaveChanges();
            }
        }
    }
}