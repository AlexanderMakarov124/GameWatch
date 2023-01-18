using System.Data.Common;
using AutoMapper;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common;
using GameWatch.UseCases.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;

/// <summary>
/// Game API controller.
/// </summary>
[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly ApplicationContext db;
    private readonly ILogger<GameController> logger;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameController(ApplicationContext db, ILogger<GameController> logger, IMapper mapper)
    {
        this.db = db;
        this.logger = logger;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET all games from database.
    /// </summary>
    /// <returns>All games.</returns>
    [HttpGet]
    [Produces("application/json")]
    public IEnumerable<Game> GetAllGames()
    {
        var games = db.Games;

        logger.Log(LogLevel.Debug, "All games was successfully retrieved.");

        return games;
    }

    /// <summary>
    /// GET a game by name.
    /// </summary>
    /// <param name="name">Game's name.</param>
    /// <returns>A game.</returns>
    [HttpGet("{name}")]
    [Produces("application/json")]
    public Game GetGameByName(string name)
    {
        var game = db.Games.FirstOrDefault(g => g.Name!.ToLower().Equals(name.ToLower()));

        if (game == null)
        {
            var message = $"Game with name {name} does not exist.";

            var exception = new NotFoundException(message);

            logger.Log(LogLevel.Error, message, exception);

            throw exception;
        }

        return game;
    }

    /// <summary>
    /// POST: Creates a game.
    /// </summary>
    /// <param name="gameDto">Game DTO.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPost]
    public ActionResult CreateGame(GameDto gameDto)
    {
        var game = mapper.Map<Game>(gameDto);

        var gameList = db.GameLists.FirstOrDefault(gl => gl.Name.ToLower().Equals(gameDto.GameListName.ToLower()));

        if (gameList == null)
        {
            var message = $"Game list with name {gameDto.Name} does not exist.";

            var exception = new NotFoundException(message);

            logger.Log(LogLevel.Error, message, exception);

            throw exception;
        }

        db.Entry(gameList).Collection(gl => gl.Games).Load();
        gameList.Games.Add(game);

        db.SaveChanges();

        logger.LogDebug(
            "Game {Name} with id {Id} was successfully created in game list {GameListName}.",
            game.Name,
            game.Id,
            gameDto.GameListName);

        return Ok();
    }

    /// <summary>
    /// PUT: Updates game.
    /// </summary>
    /// <param name="game">Updated game.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPut]
    public ActionResult UpdateGame(Game game)
    {
        db.Update(game);
        db.SaveChanges();

        logger.LogDebug("Game with id {Id} was successfully updated.", game.Id);

        return Ok();
    }

    /// <summary>
    /// DELETE: Deletes game.
    /// </summary>
    /// <param name="name">Name of the game to delete.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpDelete("{name}")]
    public ActionResult DeleteGame(string name)
    {
        var game = GetGameByName(name);
        db.Games.Remove(game);
        db.SaveChanges();

        logger.LogDebug("Game {Name} with id {Id} was successfully deleted.", game.Name, game.Id);

        return Ok();
    }
}
