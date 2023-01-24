using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.GameLists.GetGameListById;

/// <summary>
/// Query to get game list by id.
/// </summary>
public record GetGameListByIdQuery : IRequest<GameList>
{
    /// <summary>
    /// Identifier of game list.
    /// </summary>
    public int Id { get; init; }
}
