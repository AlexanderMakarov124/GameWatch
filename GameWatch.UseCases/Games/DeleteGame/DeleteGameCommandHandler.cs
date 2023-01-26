using GameWatch.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.Games.DeleteGame;

/// <summary>
/// Handler to delete game command.
/// </summary>
public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Unit>
{
    private readonly ApplicationContext db;
    private readonly ILogger<DeleteGameCommandHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteGameCommandHandler(ApplicationContext db, ILogger<DeleteGameCommandHandler> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await db.Games.FirstAsync(g => g.Name.ToLower().Equals(request.Name.ToLower()), cancellationToken);

        db.Games.Remove(game);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug("Game {Name} with id {Id} was successfully deleted.", game.Name, game.Id);

        return default;
    }
}
