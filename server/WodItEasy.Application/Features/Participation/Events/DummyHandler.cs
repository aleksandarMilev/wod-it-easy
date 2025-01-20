namespace WodItEasy.Application.Features.Participation.Events
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Events;
    using MediatR;

    public class DummyHandler : INotificationHandler<AthleteJoinedWorkoutEvent>
    {
        public Task Handle(
            AthleteJoinedWorkoutEvent notification, 
            CancellationToken cancellationToken) 
                => Task.CompletedTask;
    }
}
