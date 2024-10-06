using Falc.MusicPlayer.Domain.Model;

namespace Falc.MusicPlayer.Domain.Repositories;

public interface ITrackRepository
{
    Task<IEnumerable<Track>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}