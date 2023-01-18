using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.DTOs;
public record GameListDto
{
    /// <inheritdoc cref="GameList.Name"/>
    public string Name { get; init; } = null!;
}
