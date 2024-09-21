using MediatR;

namespace Falc.MusicPlayer.Application.Commands;

public static class AddAlbum
{
    public record SongDto(string Title);

    public record Command(string Title, IEnumerable<SongDto> Songs) : IRequest<Response>;

    public record Response(Guid Id);

    public class CommandHandler : IRequestHandler<AddAlbum.Command, AddAlbum.Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}