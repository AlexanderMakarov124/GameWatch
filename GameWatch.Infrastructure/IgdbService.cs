using GameWatch.Infrastructure.Abstractions;
using GameWatch.Infrastructure.Common;
using IGDB;
using IGDB.Models;
using Microsoft.Extensions.Options;

namespace GameWatch.Infrastructure;

/// <summary>
/// Service to manipulate with IGDB games.
/// </summary>
public class IgdbService : IIgdbService
{
    private readonly AppSettings appSettings;
    private readonly IGDBClient igdb;

    /// <summary>
    /// Constructor.
    /// </summary>
    public IgdbService(IOptions<AppSettings> appSettings)
    {
        this.appSettings = appSettings.Value;

        igdb = new IGDBClient(
            this.appSettings.IgdbClientId,
            this.appSettings.IgdbClientSecret
        );
    }
    public async Task<string> GetGameCoverUrl(string name)
    {
        var games = await igdb.QueryAsync<Game>(
            IGDBClient.Endpoints.Games,
            $"search \"{name}\"; fields cover.image_id; limit 1;");

        var game = games.First();

        var cover = ImageHelper
            .GetImageUrl(imageId: game.Cover.Value.ImageId, size: ImageSize.CoverBig, retina: false);

        return cover;
    }
}
