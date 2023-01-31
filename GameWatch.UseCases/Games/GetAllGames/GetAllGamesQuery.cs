using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.Games.GetAllGames;

/// <summary>
/// Query to get all games.
/// </summary>
public record GetAllGamesQuery : IRequest<IEnumerable<Game>>;
