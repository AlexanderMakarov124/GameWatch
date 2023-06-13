using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.Games.UpdateGame;

/// <summary>
/// Command to update a game.
/// </summary>
public record UpdateGameCommand : IRequest<Unit>
{
    public int Id { get; init; }

    /// <summary>
    /// Updated game.
    /// </summary>
    public UpdateGameDto Game { get; init; }
}
