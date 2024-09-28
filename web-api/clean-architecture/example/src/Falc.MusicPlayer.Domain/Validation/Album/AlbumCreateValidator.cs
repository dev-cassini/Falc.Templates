using Falc.MusicPlayer.Domain.Tooling.Validation;
using Falc.MusicPlayer.Domain.Validation.Album.Rules;

namespace Falc.MusicPlayer.Domain.Validation.Album;

public class AlbumCreateValidator : EntityValidator<Model.Album>
{
    protected override IEnumerable<IRule<Model.Album>> Rules =>
    [
        new AlbumHasBetweenOneAndFiftyTracksRule(),
        new AlbumTracksAreSequencedCorrectlyRule(),
        new TitleIsBetweenOneAndOneThousandCharactersLongRule()
    ];
}