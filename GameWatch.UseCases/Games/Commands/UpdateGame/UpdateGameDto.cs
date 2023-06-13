using GameWatch.Domain.Entities;

namespace GameWatch.UseCases.Games.Commands.UpdateGame;
/// <summary>
/// DTO for updating <see cref="Game"/>.
/// </summary>
public readonly record struct UpdateGameDto
{

    /// <inheritdoc cref="Game.Description"/>
    public string? Description { get; init; }

    /// <summary>
    /// Id of the list which this game belongs.
    /// </summary>
    public required int GameListId { get; init; }
}