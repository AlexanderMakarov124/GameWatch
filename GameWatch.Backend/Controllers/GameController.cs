using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common;
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

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameController(ApplicationContext db, ILogger<GameController> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <summary>
    /// GET: Retrieves all games from database.
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
    /// GET: Retrieve a game by name.
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
            var exception = new NotFoundException($"Game with name {name} does not exist.");

            var message = exception.Message;

            logger.Log(LogLevel.Error, message);

            throw exception;
        }

        return game;
    }


    [HttpPost]
    public void CreateGame(Game game)
    {
        db.Games.Add(game);
        db.SaveChanges();

        logger.LogDebug("Game {game} was successfully created.", game);
    }
}
