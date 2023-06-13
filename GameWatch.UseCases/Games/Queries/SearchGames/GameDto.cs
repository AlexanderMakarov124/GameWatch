using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.Games.Queries.SearchGames;

/// <summary>
/// DTO for <see cref="Game"/>.
/// </summary>
public readonly record struct GameDto()
{
    /// <inheritdoc cref="Game.Name"/>
    public required string Name { get; init; }

    /// <inheritdoc cref="Game.Genres"/>
    public ICollection<string> Genres { get; init; } = null!;

    /// <inheritdoc cref="Game.CreatedAt"/>
    public DateTime CreatedAt { get; init; } = default;
}
