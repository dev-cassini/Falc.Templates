using Falc.MusicPlayer.Domain.Model;
using Falc.MusicPlayer.Domain.Repositories;
using MediatR;

namespace Falc.MusicPlayer.Application.Commands;

public static class AddAlbum
{
    public record TrackDto(Guid Id);

    public record Command(string Title, IEnumerable<TrackDto> Tracks) : IRequest<Response>;

    public record Response(Guid Id);

    public class TrackDoesNotExistException(Command command, List<Track> tracks) : Exceptions.BadRequestException(
        new Dictionary<string, string[]>
        {
            {
                nameof(Command.Tracks),
                command.Tracks
                    .Where(x => tracks.Select(y => y.Id).Contains(x.Id) is false)
                    .Select(x => $"Track {x} does not exist.").ToArray()
            }
        });

    public class CommandHandler(ITrackRepository trackRepository) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
        {
            var tracks = (await trackRepository.GetAsync(command.Tracks.Select(x => x.Id), cancellationToken)).ToList();
            if (tracks.Count != command.Tracks.Count())
            {
                throw new TrackDoesNotExistException(command, tracks);
            }
            
            var album = await Album.CreateAsync(
                Guid.NewGuid(),
                command.Title,
                tracks,
                cancellationToken);

            return new Response(album.Id);
        }
    }
}