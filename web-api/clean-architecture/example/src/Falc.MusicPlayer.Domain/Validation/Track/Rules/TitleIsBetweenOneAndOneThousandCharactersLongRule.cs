using Falc.MusicPlayer.Domain.Tooling.Validation;

namespace Falc.MusicPlayer.Domain.Validation.Track.Rules;

public class TitleIsBetweenOneAndOneThousandCharactersLongRule : IRule<Model.Track>
{
    public Task CheckAsync(Model.Track track, CancellationToken cancellationToken)
    {
        return track.Title.Length switch
        {
            0 => throw new TitleIsEmptyStringException(),
            > 1000 => throw new TitleIsLongerThanOneThousandCharactersException(),
            _ => Task.CompletedTask
        };
    }
}

public class TitleIsEmptyStringException : BrokenRuleException;

public class TitleIsLongerThanOneThousandCharactersException : BrokenRuleException;