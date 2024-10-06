namespace Falc.MusicPlayer.Application;

public static class Exceptions
{
    public abstract class BadRequestException(IDictionary<string, string[]> Errors) : Exception;
}