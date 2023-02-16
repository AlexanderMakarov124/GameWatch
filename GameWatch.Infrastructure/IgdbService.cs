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

    public async Task<Game> GetGameByNameAsync(string name)
    {
        var query =
            $"search \"{name}\"; fields name, first_release_date, genres.name, summary, cover.image_id;";

        var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);

        var game = games.First(g => g.Name.ToLower().Equals(name.ToLower()));

        return game;
    }

    public string GetCoverUrl(Game game)
    {
        var cover = ImageHelper
            .GetImageUrl(imageId: game.Cover.Value.ImageId, size: ImageSize.CoverBig, retina: false);

        return cover;
    }

    public DateTime GetFirstDateRelease(Game game)
    {
        var date = game.FirstReleaseDate!.Value.Date;

        return date;
    }

    public async Task<string> GetStoreLinkAsync(Game game)
    {
        var query =  "fields category,url; " +
                     $"where game = ${game.Id} & " +
                     "category = (" +
                     $"{WebsiteCategory.Official}," +
                     $"{WebsiteCategory.Steam}," +
                     $"{WebsiteCategory.EpicGames}," +
                     $"{WebsiteCategory.GOG});";

        var websites = await igdb.QueryAsync<Website>(IGDBClient.Endpoints.Websites, query);

        var website = "";

        if (websites.Any())
        {
            website = websites.First().Url;
        }

        return website;
    }
}
