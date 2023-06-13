using MediatR;

namespace GameWatch.UseCases.Games.Queries.SearchGames;

/// <summary>
/// Query to search games.
/// </summary>
/// <param name="Name">Game name to search.</param>
public readonly record struct SearchGamesQuery(string? Name) : IRequest<IEnumerable<GameDto>>;
