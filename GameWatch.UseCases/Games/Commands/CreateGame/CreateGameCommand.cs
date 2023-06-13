using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.Games.Commands.CreateGame;

/// <summary>
/// Command to create a game.
/// </summary>
public record CreateGameCommand : IRequest<Game>
{
    /// <summary>
    /// Create game DTO.
    /// </summary>
    public CreateGameDto GameDto { get; init; }
}
