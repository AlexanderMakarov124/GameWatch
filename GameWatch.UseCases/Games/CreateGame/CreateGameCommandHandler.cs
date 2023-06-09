using AutoMapper;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure;
using GameWatch.Infrastructure.Abstractions;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.UseCases.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IGDB;
using Game = GameWatch.Domain.Entities.Game;

namespace GameWatch.UseCases.Games.CreateGame;

/// <summary>
/// Handler to <see cref="CreateGameCommand"/>.
/// </summary>
public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
{
    private readonly ApplicationContext db;
    private readonly ILogger<CreateGameCommandHandler> logger;
    private readonly IMapper mapper;
    private readonly IIgdbService igdb;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateGameCommandHandler(
        ApplicationContext db, ILogger<CreateGameCommandHandler> logger, IMapper mapper, IIgdbService igdb)
    {
        this.db = db;
        this.logger = logger;
        this.mapper = mapper;
        this.igdb = igdb;
    }

    /// <inheritdoc />
    public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var gameDto = request.GameDto;

        var gameList = await db.GameLists
            .FirstOrDefaultAsync(
                gl => gl.Name.Equals(gameDto.GameListName), cancellationToken);

        if (gameList == null)
        {
            logger.LogError("Game list with name {GameListName} does not exist.", gameDto.GameListName);

            throw new NotFoundException($"Game list with name {gameDto.GameListName} does not exist.");
        }

        await db.Entry(gameList).Collection(gl => gl.Games).LoadAsync(cancellationToken);

        var game = mapper.Map<Game>(gameDto);

        var igdbGame = await igdb.GetGameByNameAsync(game.Name);

        game.CoverUrl = igdb.GetCoverUrl(igdbGame);
        game.Summary = igdbGame.Summary;
        game.ReleaseDate = igdb.GetFirstReleaseDate(igdbGame);
        game.StoreLink = await igdb.GetStoreLinkAsync(igdbGame);

        game.Genres = new List<Genre>();

        foreach (var igdbGenre in igdbGame.Genres.Values)
        {
            var genre = new Genre
            {
                Name = igdbGenre.Name
            };

            game.Genres.Add(genre);
        }

        gameList.Games.Add(game);

        await db.SaveChangesAsync(cancellationToken);

        logger.LogDebug(
            "Game {Name} with id {Id} was successfully created in game list {GameListName}.",
            game.Name,
            game.Id,
            gameDto.GameListName);

        return game;
    }
}
