using Falc.MusicPlayer.Domain.Events.Album;
using MediatR;
using AlbumTrackDto = Falc.MusicPlayer.Domain.Contracts.AlbumTrackDto;

namespace Falc.MusicPlayer.Application.SideEffects.IntegrationEvents;

public static class PublishAlbumCreatedIntegrationEvent
{
    public class CommandHandler(IEventPublisher eventPublisher) : INotificationHandler<AlbumCreated>
    {
        public async Task Handle(AlbumCreated notification, CancellationToken cancellationToken)
        {
            var @event = new Domain.Contracts.AlbumCreated(
                notification.Id,
                notification.Title,
                notification.Tracks.Select(x => new AlbumTrackDto(x.Number, x.TrackId)));
            
            await eventPublisher.PublishAsync(@event, cancellationToken);
        }
    }
}