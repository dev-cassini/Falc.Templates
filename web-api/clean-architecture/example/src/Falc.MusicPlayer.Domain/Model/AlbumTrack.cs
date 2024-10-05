using Falc.MusicPlayer.Domain.Validation.AlbumTrack;

namespace Falc.MusicPlayer.Domain.Model;

public class AlbumTrack
{
    public int Number { get; }
    
    public Guid AlbumId { get; }
    
    public Guid TrackId { get; }

    private AlbumTrack(int number, Album album, Track track)
    {
        Number = number;
        AlbumId = album.Id;
        TrackId = track.Id;
    }

    public static async Task<AlbumTrack> CreateAsync(
        int number,
        Album album,
        Track track,
        CancellationToken cancellationToken)
    {
        var albumTrack = new AlbumTrack(number, album, track);
        await new AlbumTrackCreateValidator().ValidateAsync(albumTrack, cancellationToken);

        return albumTrack;
    }
}