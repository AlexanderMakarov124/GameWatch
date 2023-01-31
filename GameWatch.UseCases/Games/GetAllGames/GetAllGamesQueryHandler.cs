using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.UseCases.Games.GetAllGames;

/// <summary>
/// Handler to get all games query.
/// </summary>
public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Game>>
{
    private readonly ApplicationContext db;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllGamesQueryHandler(ApplicationContext db)
    {
        this.db = db;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        return await db.Games.ToListAsync(cancellationToken);
    }
}
