using Falc.MusicPlayer.Domain.Builders.Model;
using Falc.MusicPlayer.Domain.Model;
using Falc.MusicPlayer.Domain.Tooling.Validation;
using Falc.MusicPlayer.Domain.Validation.Album.Rules;
using Falc.MusicPlayer.Domain.Validation.AlbumTrack.Rules;

namespace Falc.MusicPlayer.Domain.UnitTests.Model;

[TestFixture]
public class AlbumTests
{
    [Test]
    public void WhenAlbumHasNoTracks_ThenExceptionIsThrown()
    {
        var aggregatedException = Assert.ThrowsAsync<AggregatedBrokenRuleException>(async () =>
        {
            await Album.CreateAsync(Guid.NewGuid(), "Test Album", [], CancellationToken.None);
        });
        
        Assert.That(aggregatedException.Exceptions.OfType<AlbumHasNoTracksException>().Count(), Is.EqualTo(1));
    }
    
    [Test]
    public async Task WhenAlbumHasMoreThan50Tracks_ThenExceptionIsThrown()
    {
        var tracks = new List<Track>();
        for (var i = 1; i <= 51; i++)
        {
            var track = await new TrackBuilder().BuildAsync();
            tracks.Add(track);
        }
        
        var aggregatedException = Assert.ThrowsAsync<AggregatedBrokenRuleException>(async () =>
        {
            await Album.CreateAsync(Guid.NewGuid(), "Test Album", tracks, CancellationToken.None);
        });
        
        Assert.That(aggregatedException.Exceptions.OfType<NumberIsGreaterThanFiftyException>().Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void WhenAlbumTitleIsEmptyString_ThenExceptionIsThrown()
    {
        var aggregatedException = Assert.ThrowsAsync<AggregatedBrokenRuleException>(async () =>
        {
            var track = await new TrackBuilder().BuildAsync();
            await Album.CreateAsync(Guid.NewGuid(), string.Empty, [track], CancellationToken.None);
        });
        
        Assert.That(aggregatedException.Exceptions.OfType<TitleIsEmptyStringException>().Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void WhenAlbumTitleIsMoreThan1000CharactersLong_ThenExceptionIsThrown()
    {
        var aggregatedException = Assert.ThrowsAsync<AggregatedBrokenRuleException>(async () =>
        {
            var title = string.Concat(Enumerable.Repeat("a", 1001));
            var track = await new TrackBuilder().BuildAsync();
            await Album.CreateAsync(Guid.NewGuid(), title, [track], CancellationToken.None);
        });
        
        Assert.That(aggregatedException.Exceptions.OfType<TitleIsLongerThanOneThousandCharactersException>().Count(), Is.EqualTo(1));
    }
    
    [Test]
    public async Task WhenHappyPath_ThenIdIsSet()
    {
        var track1 = await new TrackBuilder().BuildAsync();
        var id = Guid.NewGuid();
        var album = await Album.CreateAsync(
            id, 
            $"Test Album {Guid.NewGuid()}", 
            [track1], 
            CancellationToken.None);
        
        Assert.That(album.Id, Is.EqualTo(id));
    }
    
    [Test]
    public async Task WhenHappyPath_ThenTitleIsSet()
    {
        var track1 = await new TrackBuilder().BuildAsync();
        var title = $"Test Album {Guid.NewGuid()}";
        var album = await Album.CreateAsync(
            Guid.NewGuid(), 
            title, 
            [track1], 
            CancellationToken.None);
        
        Assert.That(album.Title, Is.EqualTo(title));
    }
    
    [Test]
    public async Task WhenHappyPath_ThenTracksIsSet()
    {
        var track1 = await new TrackBuilder().BuildAsync();
        var track2 = await new TrackBuilder().BuildAsync();
        var track3 = await new TrackBuilder().BuildAsync();
        
        var album = await Album.CreateAsync(
            Guid.NewGuid(), 
            $"Test Album {Guid.NewGuid()}", 
            [track1, track2, track3], 
            CancellationToken.None);
        
        Assert.That(album.Tracks, Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(album.Tracks.Count(x => x.TrackId == track1.Id), Is.EqualTo(1));
            Assert.That(album.Tracks.Count(x => x.TrackId == track2.Id), Is.EqualTo(1));
            Assert.That(album.Tracks.Count(x => x.TrackId == track3.Id), Is.EqualTo(1));
        });
    }
    
    [Test]
    public async Task WhenHappyPath_ThenTracksAreSequencedCorrectly()
    {
        var track1 = await new TrackBuilder().BuildAsync();
        var track2 = await new TrackBuilder().BuildAsync();
        var track3 = await new TrackBuilder().BuildAsync();
        
        var album = await Album.CreateAsync(
            Guid.NewGuid(), 
            $"Test Album {Guid.NewGuid()}", 
            [track1, track2, track3], 
            CancellationToken.None);
        
        var albumTrack1 = album.Tracks.Single(x => x.TrackId == track1.Id);
        Assert.That(albumTrack1.Number, Is.EqualTo(1));
        
        var albumTrack2 = album.Tracks.Single(x => x.TrackId == track2.Id);
        Assert.That(albumTrack2.Number, Is.EqualTo(2));
        
        var albumTrack3 = album.Tracks.Single(x => x.TrackId == track3.Id);
        Assert.That(albumTrack3.Number, Is.EqualTo(3));
    }
}