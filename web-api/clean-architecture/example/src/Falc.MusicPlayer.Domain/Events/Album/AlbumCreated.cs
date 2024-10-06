using MediatR;

namespace Falc.MusicPlayer.Domain.Events.Album;

public record AlbumCreated(
    Guid Id,
    string Title,
    IEnumerable<AlbumTrackDto> Tracks) : INotification, IIntegrationEvent<Contracts.AlbumCreated>
{
    public Contracts.AlbumCreated MapToContract()
    {
        return new Contracts.AlbumCreated(
            Id,
            Title,
            Tracks.Select(x => new Contracts.AlbumTrackDto(x.Number, x.TrackId)));
    }
}

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