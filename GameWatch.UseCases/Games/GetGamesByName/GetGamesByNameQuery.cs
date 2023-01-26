using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.Games.GetGamesByName;

/// <summary>
/// Query to get games by name.
/// </summary>
public record GetGamesByNameQuery : IRequest<IEnumerable<Game>>
{
    /// <summary>
    /// Name to find.
    /// </summary>
    public string Name { get; init; } = null!;
}
