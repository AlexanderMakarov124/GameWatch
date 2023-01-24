using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.Games.UpdateGame;

/// <summary>
/// Command to update a game.
/// </summary>
public record UpdateGameCommand : IRequest
{
    /// <summary>
    /// Updated game.
    /// </summary>
    public Game Game { get; init; } = null!;
}
