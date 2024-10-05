using Falc.MusicPlayer.Domain.Tooling.Validation;
using Falc.MusicPlayer.Domain.Validation.Track.Rules;
using TitleIsBetweenOneAndOneThousandCharactersLongRule = Falc.MusicPlayer.Domain.Validation.Track.Rules.TitleIsBetweenOneAndOneThousandCharactersLongRule;

namespace Falc.MusicPlayer.Domain.Validation.Track;

public class TrackCreateValidator : EntityValidator<Model.Track>
{
    protected override IEnumerable<IRule<Model.Track>> Rules =>
    [
        new TitleIsBetweenOneAndOneThousandCharactersLongRule(),
        new TrackLengthIsBetweenOneSecondAndOneHourRule()
    ];
}