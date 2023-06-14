using MediatR;

namespace GameWatch.UseCases.Games.Commands.UpdateGame;

/// <summary>
/// Command to update a game.
/// </summary>
public record UpdateGameCommand : IRequest<Unit>
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Updated game.
    /// </summary>
    public UpdateGameDto Game { get; init; }
}
