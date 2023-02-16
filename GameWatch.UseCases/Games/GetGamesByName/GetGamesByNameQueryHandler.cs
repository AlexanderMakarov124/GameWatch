using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.UseCases.Games.GetGamesByName;

/// <summary>
/// Handler to get games by name query.
/// </summary>
public class GetGamesByNameQueryHandler : IRequestHandler<GetGamesByNameQuery, IEnumerable<Game>>
{
    private readonly ApplicationContext db;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetGamesByNameQueryHandler(ApplicationContext db)
    {
        this.db = db;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Game>> Handle(GetGamesByNameQuery request, CancellationToken cancellationToken)
    {
        var games = await db.Games
            .Include(g => g.Genres)
            .Where(g => g.Name.ToLower().Contains(request.Name.ToLower()))
            .ToListAsync(cancellationToken);

        return games;
    }
}
