using GameWatch.UseCases.DTOs;
using MediatR;

namespace GameWatch.UseCases.Games.CreateGame;

/// <summary>
/// Command to create a game.
/// </summary>
public record CreateGameCommand : IRequest
{
    /// <summary>
    /// Game DTO.
    /// </summary>
    public GameDto GameDto { get; init; } = null!;
}
