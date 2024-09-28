using Falc.MusicPlayer.Domain.Tooling.Validation;

namespace Falc.MusicPlayer.Domain.Validation.Album.Rules;

public class TitleIsBetweenOneAndOneThousandCharactersLongRule : IRule<Model.Album>
{
    public Task CheckAsync(Model.Album album, CancellationToken cancellationToken)
    {
        return album.Title.Length switch
        {
            0 => throw new TitleIsEmptyStringException(),
            > 1000 => throw new TitleIsLongerThanOneThousandCharactersException(),
            _ => Task.CompletedTask
        };
    }
}

public class TitleIsEmptyStringException : BrokenRuleException;

public class TitleIsLongerThanOneThousandCharactersException : BrokenRuleException;