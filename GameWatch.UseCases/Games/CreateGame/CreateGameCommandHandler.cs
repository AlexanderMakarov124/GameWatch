using AutoMapper;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameWatch.UseCases.Games.CreateGame;

/// <summary>
/// Handler to create game command.
/// </summary>
public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Unit>
{
    private readonly ApplicationContext db;
    private readonly ILogger<CreateGameCommandHandler> logger;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateGameCommandHandler(ApplicationContext db, ILogger<CreateGameCommandHandler> logger, IMapper mapper)
    {
        this.db = db;
        this.logger = logger;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var gameDto = request.GameDto;

        var gameList = await db.GameLists
            .FirstOrDefaultAsync(gl => gl.Name.ToLower().Equals(gameDto.GameListName.ToLower()), cancellationToken);

        if (gameList == null)
        {
            logger.LogError("Game list with name {GameListName} does not exist.", gameDto.GameListName);

            throw new NotFoundException($"Game list with name {gameDto.GameListName} does not exist.");
        }

        await db.Entry(gameList).Collection(gl => gl.Games).LoadAsync(cancellationToken);

        var game = mapper.Map<Game>(gameDto);
        gameList.Games.Add(game);

        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug(
            "Game {Name} with id {Id} was successfully created in game list {GameListName}.",
            game.Name,
            game.Id,
            gameDto.GameListName);

        return default;
    }
}
