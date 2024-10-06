namespace Falc.MusicPlayer.Domain.Contracts;

public record AlbumCreated(
    Guid Id,
    string Title,
    IEnumerable<AlbumTrackDto> Tracks);
    
public record AlbumTrackDto(int Number, Guid TrackId);