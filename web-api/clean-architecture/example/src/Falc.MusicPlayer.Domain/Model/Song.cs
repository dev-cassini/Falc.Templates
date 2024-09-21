namespace Falc.MusicPlayer.Domain.Model;

public class Song
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// The title of the song.
    /// </summary>
    public string Title { get; }

    public Song(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
}