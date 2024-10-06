using MediatR;

namespace Falc.MusicPlayer.Domain.Events.Album;

public record AlbumCreated(
    Guid Id,
    string Title,
    IEnumerable<AlbumTrackDto> Tracks) : INotification;
    
public record AlbumTrackDto(int Number, Guid TrackId);

internal static class AlbumExtensions
{
    internal static AlbumCreated MapToCreatedDomainEvent(this Model.Album album)
    {
        return new AlbumCreated(
            album.Id,
            album.Title,
            album.Tracks.Select(x => new AlbumTrackDto(x.Number, x.TrackId)));
    }
}