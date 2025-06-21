namespace WodItEasy.Common.Domain.Events
{
    public record UserRegisteredEvent(
        string Email,
        string Name) : IDomainEvent
    { }
}
