﻿using GameWatch.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.Games.UpdateGame;

/// <summary>
/// Handler to update game command.
/// </summary>
public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
{
    private readonly ApplicationContext db;
    private readonly ILogger<UpdateGameCommandHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateGameCommandHandler(ApplicationContext db, ILogger<UpdateGameCommandHandler> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = request.Game;

        db.Update(game);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug("Game with id {Id} was successfully updated.", game.Id);

        return default;
    }
}