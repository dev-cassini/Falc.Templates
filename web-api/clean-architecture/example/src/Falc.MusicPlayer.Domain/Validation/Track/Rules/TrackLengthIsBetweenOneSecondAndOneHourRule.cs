using Falc.MusicPlayer.Domain.Tooling.Validation;

namespace Falc.MusicPlayer.Domain.Validation.Track.Rules;

public class TrackLengthIsBetweenOneSecondAndOneHourRule : IRule<Model.Track>
{
    public Task CheckAsync(Model.Track track, CancellationToken cancellationToken)
    {
        return track.LengthInSeconds switch
        {
            0 => throw new TrackLengthIsZeroException(),
            > 3600 => throw new TrackLengthExceedsOneHourException(),
            _ => Task.CompletedTask
        };
    }
}

public class TrackLengthIsZeroException : BrokenRuleException;

public class TrackLengthExceedsOneHourException : BrokenRuleException;