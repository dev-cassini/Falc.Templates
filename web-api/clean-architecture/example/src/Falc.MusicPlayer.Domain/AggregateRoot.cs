using MediatR;

namespace Falc.MusicPlayer.Domain;

public abstract class AggregateRoot
{
    private readonly List<INotification> _domainEvents = [];
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(INotification @event)
    {
        _domainEvents.Add(@event);
    }
}