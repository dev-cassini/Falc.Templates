using Falc.MusicPlayer.Domain.Tooling.Validation;

namespace Falc.MusicPlayer.Domain.Validation.AlbumTrack.Rules;

public class NumberIsBetweenOneAndFiftyRule : IRule<Model.AlbumTrack>
{
    public Task CheckAsync(Model.AlbumTrack albumTrack, CancellationToken cancellationToken)
    {
        return albumTrack.Number switch
        {
            0 => throw new NumberIsZeroOrLessException(),
            > 50 => throw new NumberIsGreaterThanFiftyException(),
            _ => Task.CompletedTask
        };
    }
}

public class NumberIsZeroOrLessException : BrokenRuleException;

public class NumberIsGreaterThanFiftyException : BrokenRuleException;