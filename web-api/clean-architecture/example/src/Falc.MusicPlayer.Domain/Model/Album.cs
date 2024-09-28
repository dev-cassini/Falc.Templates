using Falc.MusicPlayer.Domain.Validation.Album;

namespace Falc.MusicPlayer.Domain.Model;

public class Album
{
    public Guid Id { get; }
    
    public string Title { get; }
    
    public IEnumerable<AlbumTrack> Tracks { get; }

    private Album(Guid id, string title, IEnumerable<AlbumTrack> tracks)
    {
        Id = id;
        Title = title;
        Tracks = tracks;
    }

    public static async Task<Album> CreateAsync(
        Guid id, 
        string title, 
        IEnumerable<AlbumTrack> tracks,
        CancellationToken cancellationToken)
    {
        var album = new Album(id, title, tracks);
        await new AlbumCreateValidator().ValidateAsync(album, cancellationToken);

        return album;
    }
}