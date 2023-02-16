using IGDB.Models;

namespace GameWatch.Infrastructure.Abstractions;
public interface IIgdbService
{
    Task<Game> GetGameByNameAsync(string name);
    string GetCoverUrl(Game game);
    DateTime GetFirstDateRelease(Game game);
    Task<string> GetStoreLinkAsync(Game game);
}
