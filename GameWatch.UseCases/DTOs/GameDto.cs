using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.DTOs;
public record GameDto
{
    /// <inheritdoc cref="Game.Name"/>
    public string Name { get; init; } = null!;

    /// <inheritdoc cref="Game.Description"/>
    public string? Description { get; init; }

    /// <summary>
    /// Name of the list which this game belongs.
    /// </summary>
    public string GameListName { get; init; } = null!;

    /// <inheritdoc cref="Game.DownloadLink"/>
    public string? DownloadLink { get; init; }
}
