namespace Falc.MusicPlayer.Domain.Model;

public class Track
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// The title of the track.
    /// </summary>
    public string Title { get; }
    
    /// <summary>
    /// How many seconds the track runs for.
    /// </summary>
    public int LengthInSeconds { get; }

    public Track(Guid id, string title, int lengthInSeconds)
    {
        Id = id;
        Title = title;
        LengthInSeconds = lengthInSeconds;
    }
}