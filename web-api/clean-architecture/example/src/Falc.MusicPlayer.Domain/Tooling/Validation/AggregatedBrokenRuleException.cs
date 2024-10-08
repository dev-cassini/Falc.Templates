namespace Falc.MusicPlayer.Domain.Tooling.Validation;

/// <summary>
/// A collection of <see cref="BrokenRuleException"/> thrown when the exceptions of multiple
/// rule checks applicable to an entity need to be aggregated.
/// </summary>
public class AggregatedBrokenRuleException : Exception
{
    private readonly List<BrokenRuleException> _exceptions = [];
    public IReadOnlyList<BrokenRuleException> Exceptions => _exceptions;

    public void AddException(BrokenRuleException exception)
    {
        _exceptions.Add(exception);
    }
}