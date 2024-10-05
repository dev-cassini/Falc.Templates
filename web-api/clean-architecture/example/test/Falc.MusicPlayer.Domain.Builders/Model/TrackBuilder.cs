using Falc.MusicPlayer.Domain.Model;

namespace Falc.MusicPlayer.Domain.Builders.Model;

public class TrackBuilder
{
    private string _title = $"Track {Guid.NewGuid()}";
    private int _lengthInSeconds = 60;
    
    public async Task<Track> BuildAsync()
    {
        return await Track.CreateAsync(
            Guid.NewGuid(),
            _title,
            _lengthInSeconds,
            CancellationToken.None);
    }

    public TrackBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public TrackBuilder WithLengthInSeconds(int lengthInSeconds)
    {
        _lengthInSeconds = lengthInSeconds;
        return this;
    }
}