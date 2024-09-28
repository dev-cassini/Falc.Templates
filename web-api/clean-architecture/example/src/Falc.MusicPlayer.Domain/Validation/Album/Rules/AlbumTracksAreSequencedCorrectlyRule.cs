using Falc.MusicPlayer.Domain.Tooling.Validation;

namespace Falc.MusicPlayer.Domain.Validation.Album.Rules;

public class AlbumTracksAreSequencedCorrectlyRule : IRule<Model.Album>
{
    public Task CheckAsync(Model.Album album, CancellationToken cancellationToken)
    {
        var orderedAlbumTrackNumbers = album.Tracks
            .Select(x => x.Number)
            .Order().ToList();

        var isConsecutive = orderedAlbumTrackNumbers
            .Zip(orderedAlbumTrackNumbers.Skip(1), (x, y) => x + 1 == y)
            .All(x => x);

        if (isConsecutive)
        {
            return Task.CompletedTask;
        }

        throw new AlbumTracksAreNotSequencedCorrectlyException();
    }
}

public class AlbumTracksAreNotSequencedCorrectlyException : BrokenRuleException;