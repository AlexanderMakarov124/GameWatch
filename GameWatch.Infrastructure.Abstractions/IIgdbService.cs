namespace GameWatch.Infrastructure.Abstractions;
public interface IIgdbService
{
    Task<string> GetGameCoverUrl(string name);
}
