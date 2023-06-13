using GameWatch.Domain.Entities;
using GameWatch.UseCases.Games.Queries.Common;

namespace GameWatch.UseCases.Games.Queries.SearchGames;

/// <summary>
/// DTO for <see cref="Game"/>.
/// </summary>
public readonly record struct GameDto
{
    /// <inheritdoc cref="Game.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Game.Name"/>
    public required string Name { get; init; }

    /// <inheritdoc cref="Game.Genres"/>
    public IEnumerable<GenreDto> Genres { get; init; }

    /// <inheritdoc cref="Game.CreatedAt"/>
    public DateTime CreatedAt { get; init; }
}
