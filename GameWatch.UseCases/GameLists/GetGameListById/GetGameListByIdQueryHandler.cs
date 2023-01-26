using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.GameLists.GetGameListById;

/// <summary>
/// Handler for get game list by id query.
/// </summary>
public class GetGameListByIdQueryHandler : IRequestHandler<GetGameListByIdQuery, GameList>
{
    private readonly ApplicationContext db;
    private readonly ILogger<GetGameListByIdQueryHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetGameListByIdQueryHandler(ApplicationContext db, ILogger<GetGameListByIdQueryHandler> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<GameList> Handle(GetGameListByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var gameList = await db.GameLists.FirstOrDefaultAsync(gl => gl.Id == id, cancellationToken);

        if (gameList == null)
        {
            logger.LogError("Game list with id {Id} does not exist.", id);

            throw new NotFoundException($"Game list with id {id} does not exist.");
        }

        await db.Entry(gameList).Collection(gl => gl.Games).LoadAsync(cancellationToken);

        return gameList;
    }
}
