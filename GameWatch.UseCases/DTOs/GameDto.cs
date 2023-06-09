using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.DTOs;

/// <summary>
/// DTO for <see cref="Game"/>.
/// </summary>
public record GameDto
{
    /// <inheritdoc cref="Game.Name"/>
    public required string Name { get; init; }

    /// <inheritdoc cref="Game.Description"/>
    public string? Description { get; init; }

    /// <summary>
    /// Name of the list which this game belongs.
    /// </summary>
    public required string GameListName { get; init; }

    /// <inheritdoc cref="Game.DownloadLink"/>
    public string? DownloadLink { get; init; }
}
