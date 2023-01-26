using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.GameLists.GetAllGameLists;

/// <summary>
/// Handler for get all game lists query.
/// </summary>
public class GetAllGameListsQueryHandler : IRequestHandler<GetAllGameListsQuery, IEnumerable<GameList>>
{
    private readonly ApplicationContext db;
    private readonly ILogger<GetAllGameListsQueryHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllGameListsQueryHandler(ApplicationContext db, ILogger<GetAllGameListsQueryHandler> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <inheritdoc />
    public Task<IEnumerable<GameList>> Handle(GetAllGameListsQuery request, CancellationToken cancellationToken)
    {
        var lists = db.GameLists.Include(gl => gl.Games);

        logger.LogDebug("All game lists was successfully retrieved.");

        return Task.FromResult<IEnumerable<GameList>>(lists);
    }
}
