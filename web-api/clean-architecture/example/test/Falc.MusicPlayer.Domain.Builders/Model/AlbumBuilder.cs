using Falc.MusicPlayer.Domain.Model;

namespace Falc.MusicPlayer.Domain.Builders.Model;

public class AlbumBuilder
{
    private string _title = $"Album {Guid.NewGuid()}";
    private readonly List<Track> _tracks = [];
    
    public async Task<Album> BuildAsync()
    {
        return await Album.CreateAsync(
            Guid.NewGuid(),
            _title,
            _tracks,
            CancellationToken.None);
    }

    public AlbumBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public AlbumBuilder AddTrack(Track track)
    {
        _tracks.Add(track);
        return this;
    }
}