using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.GameLists.UpdateGameList;

/// <summary>
/// Command to update game list.
/// </summary>
public record UpdateGameListCommand : IRequest
{
    /// <summary>
    /// Update game list.
    /// </summary>
    public GameList GameList { get; init; } = null!;
}
