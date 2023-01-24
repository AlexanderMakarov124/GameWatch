using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.GameLists.GetAllGameLists;

/// <summary>
/// Query to get all game lists.
/// </summary>
public record GetAllGameListsQuery : IRequest<IEnumerable<GameList>>;