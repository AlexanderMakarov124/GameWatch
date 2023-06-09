using GameWatch.UseCases.DTOs;
using MediatR;

namespace GameWatch.UseCases.GameLists.CreateGameList;

/// <summary>
/// Command to create a game list.
/// </summary>
public record CreateGameListCommand : IRequest<Unit>
{
    /// <summary>
    /// Game list DTO.
    /// </summary>
    public GameListDto GameListDto { get; init; } = null!;
}
