namespace Falc.MusicPlayer.Domain.Tooling.Validation;

/// <summary>
/// A rule check applicable to the entity of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Type of entity.</typeparam>
public interface IRule<in T> where T : class
{
    /// <summary>
    /// The rule check. Throws <see cref="BrokenRuleException"/> on failure.
    /// </summary>
    /// <param name="entity">Entity to which the rule applies.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BrokenRuleException">Check failed.</exception>
    Task CheckAsync(T entity, CancellationToken cancellationToken);
}