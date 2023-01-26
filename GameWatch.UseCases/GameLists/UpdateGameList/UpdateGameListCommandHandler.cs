using GameWatch.DataAccess;
using GameWatch.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.GameLists.UpdateGameList;

/// <summary>
/// Handler to update game list command.
/// </summary>
public class UpdateGameListCommandHandler : IRequestHandler<UpdateGameListCommand, Unit>
{
    private readonly ApplicationContext db;
    private readonly ILogger<UpdateGameListCommandHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateGameListCommandHandler(ApplicationContext db, ILogger<UpdateGameListCommandHandler> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(UpdateGameListCommand request, CancellationToken cancellationToken)
    {
        var gameList = request.GameList;

        if (await db.GameLists.AnyAsync(gl => gl.Name.ToLower().Equals(gameList.Name.ToLower()), cancellationToken))
        {
            logger.LogError("Can not update game list {Name} because game list with such name already exists.", gameList.Name);

            throw new AlreadyExistException($"Can not update game list {gameList.Name} because game list with such name already exists.");
        }

        db.Update(gameList);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug("Game list with id {Id} was successfully updated.", gameList.Id);

        return default;
    }
}
