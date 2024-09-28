using Falc.MusicPlayer.Domain.Tooling.Validation;
using Falc.MusicPlayer.Domain.Validation.AlbumTrack.Rules;

namespace Falc.MusicPlayer.Domain.Validation.AlbumTrack;

public class AlbumTrackCreateValidator : EntityValidator<Model.AlbumTrack>
{
    protected override IEnumerable<IRule<Model.AlbumTrack>> Rules => 
    [
        new NumberIsBetweenOneAndFiftyRule()
    ];
}