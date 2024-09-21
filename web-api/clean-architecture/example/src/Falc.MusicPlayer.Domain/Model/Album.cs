namespace Falc.MusicPlayer.Domain.Model;

public class Album
{
    public Guid Id { get; }
    
    public string Title { get; }
    
    public IEnumerable<Song> Songs { get; }

    public Album(Guid id, string title, IEnumerable<Song> songs)
    {
        Id = id;
        Title = title;
        Songs = songs;
    }
}