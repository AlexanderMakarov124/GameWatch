using GameWatch.Domain.Entities;
using MediatR;

namespace GameWatch.UseCases.Games.GetGameById;

/// <summary>
/// Query to get game by id.
/// </summary>
public record GetGameByIdQuery : IRequest<Game>
{
    /// <summary>
    /// Game identifier.
    /// </summary>
    public int Id { get; init; }
}
