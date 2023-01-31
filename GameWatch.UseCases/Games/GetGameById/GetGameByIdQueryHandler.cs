using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.UseCases.Games.GetGameById;

/// <summary>
/// Handler to get game by id query.
/// </summary>
public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Game>
{
    private readonly ApplicationContext db;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetGameByIdQueryHandler(ApplicationContext db)
    {
        this.db = db;
    }

    /// <inheritdoc />
    public async Task<Game> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var game = await db.Games.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if (game == null)
        {
            throw new NotFoundException($"Game with id {id} does not exist.");
        }

        return game;
    }
}
