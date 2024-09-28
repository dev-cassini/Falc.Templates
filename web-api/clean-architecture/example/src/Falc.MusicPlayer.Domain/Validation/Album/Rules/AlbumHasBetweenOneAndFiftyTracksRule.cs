using Falc.MusicPlayer.Domain.Tooling.Validation;

namespace Falc.MusicPlayer.Domain.Validation.Album.Rules;

public class AlbumHasBetweenOneAndFiftyTracksRule : IRule<Model.Album>
{
    public Task CheckAsync(Model.Album album, CancellationToken cancellationToken)
    {
        return album.Tracks.Count() switch
        {
            0 => throw new AlbumHasNoTracksException(),
            > 50 => throw new AlbumHasMoreThanFiftyTracksException(),
            _ => Task.CompletedTask
        };
    }
}

public class AlbumHasNoTracksException : BrokenRuleException;

public class AlbumHasMoreThanFiftyTracksException : BrokenRuleException;