namespace WodItEasy.Identity.Infrastructure.Persistence
{
    using System.Reflection;
    using Common.Domain.Models;
    using Common.Infrastructure.Events;
    using Common.Infrastructure.Configuration;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class IdentityDbContext(
        DbContextOptions<IdentityDbContext> options,
        IEventPublisher eventPublisher)
        : IdentityDbContext<User>(options)
    {
        private readonly IEventPublisher eventPublisher = eventPublisher;

        public DbSet<Message> Messages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entitiesWithEvents = ChangeTracker
                .Entries<IEntity>() 
                .Select(e => e.Entity)
                .Where(e => e.Events.Count > 0)
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var toPublish = entity.Events
                    .Select(evt => new Message(evt))
                    .ToList();

                entity.ClearEvents();

                this.Messages.AddRange(toPublish);
            }

            await base.SaveChangesAsync(cancellationToken);

            var unpublishedMessageEntities = this.ChangeTracker
                .Entries<Message>()
                .Where(e => !e.Entity.Published)
                .Select(e => e.Entity);

            foreach (var message in unpublishedMessageEntities)
            {
                await this.eventPublisher.Publish(
                    message.Data,
                    message.Type);

                message.MarkAsPublished();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
