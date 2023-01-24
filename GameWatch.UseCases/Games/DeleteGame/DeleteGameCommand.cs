using MediatR;

namespace GameWatch.UseCases.Games.DeleteGame;

/// <summary>
/// Command to delete a game.
/// </summary>
public record DeleteGameCommand : IRequest
{
    /// <summary>
    /// Game's name.
    /// </summary>
    public string Name { get; init; } = null!;
}
