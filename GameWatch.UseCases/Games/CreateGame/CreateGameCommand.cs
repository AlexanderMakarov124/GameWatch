using GameWatch.Domain.Entities;
using GameWatch.UseCases.DTOs;
using MediatR;

namespace GameWatch.UseCases.Games.CreateGame;

/// <summary>
/// Command to create a game.
/// </summary>
public record CreateGameCommand : IRequest<Game>
{
    /// <summary>
    /// Game DTO.
    /// </summary>
    public GameDto GameDto { get; init; } = null!;
}
