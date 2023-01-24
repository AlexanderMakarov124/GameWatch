using MediatR;

namespace GameWatch.UseCases.GameLists.DeleteGameList;

/// <summary>
/// Command to delete a game list.
/// </summary>
public record DeleteGameListCommand : IRequest
{
    /// <summary>
    /// Identifier of a game list.
    /// </summary>
    public int Id { get; init; }
}
