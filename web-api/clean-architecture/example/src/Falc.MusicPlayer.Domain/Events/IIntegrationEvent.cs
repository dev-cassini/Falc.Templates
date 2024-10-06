using MediatR;

namespace Falc.MusicPlayer.Domain.Events;

public interface IIntegrationEvent<out T> where T : INotification
{
    T MapToContract();
}