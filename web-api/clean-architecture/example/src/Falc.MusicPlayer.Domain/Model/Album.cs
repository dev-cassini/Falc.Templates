using Falc.MusicPlayer.Domain.Validation.Album;

namespace Falc.MusicPlayer.Domain.Model;

public class Album
{
    public Guid Id { get; }
    
    public string Title { get; }

    private readonly List<AlbumTrack> _tracks;
    public IReadOnlyList<AlbumTrack> Tracks => _tracks;

    private Album(Guid id, string title)
    {
        Id = id;
        Title = title;
        _tracks = [];
    }

    public static async Task<Album> CreateAsync(
        Guid id, 
        string title, 
        IEnumerable<Track> tracks,
        CancellationToken cancellationToken)
    {
        var album = new Album(id, title);

        var albumTrackNumber = 1;
        foreach (var track in tracks)
        {
            var albumTrack = await AlbumTrack.CreateAsync(albumTrackNumber, album, track, cancellationToken);
            album._tracks.Add(albumTrack);
            albumTrackNumber++;
        }
        
        await new AlbumCreateValidator().ValidateAsync(album, cancellationToken);

        return album;
    }
}