using GameWatch.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.GameLists.DeleteGameList;

/// <summary>
/// Handler to delete game list command.
/// </summary>
internal class DeleteGameListCommandHandler : AsyncRequestHandler<DeleteGameListCommand>
{
    private readonly ApplicationContext db;
    private readonly ILogger<DeleteGameListCommandHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteGameListCommandHandler(ApplicationContext db, ILogger<DeleteGameListCommandHandler> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <inheritdoc />
    protected override async Task Handle(DeleteGameListCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var gameList = await db.GameLists.SingleAsync(gl => gl.Id == id, cancellationToken);

        await db.Entry(gameList).Collection(gl => gl.Games).LoadAsync(cancellationToken);

        if (gameList.Games.Any())
        {
            foreach (var game in gameList.Games)
            {
                db.Games.Remove(game);
            }
        }

        db.GameLists.Remove(gameList);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug("Game list {Name} with id {Id} was successfully deleted.", gameList.Name, gameList.Id);
    }
}
