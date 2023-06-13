using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.Games.Queries.Common;

/// <summary>
/// DTO for <see cref="Genre"/>.
/// </summary>
public readonly record struct GenreDto
{
    /// <inheritdoc cref="Genre.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Genre.Name"/>
    public required string Name { get; init; }
}
