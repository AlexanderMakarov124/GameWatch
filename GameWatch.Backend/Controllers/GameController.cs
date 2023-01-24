using AutoMapper;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.UseCases.DTOs;
using GameWatch.UseCases.Games.CreateGame;
using GameWatch.UseCases.Games.DeleteGame;
using GameWatch.UseCases.Games.UpdateGame;
using MediatR;
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
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameController(ApplicationContext db, ILogger<GameController> logger, IMapper mapper, IMediator mediator)
    {
        this.db = db;
        this.logger = logger;
        this.mediator = mediator;
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
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateGameAsync(GameDto gameDto, CancellationToken cancellationToken)
    {
        var command = new CreateGameCommand
        {
            GameDto = gameDto
        };

        try
        {
            await mediator.Send(command, cancellationToken);
        }
        catch (NotFoundException)
        {
            return BadRequest();
        }

        return Ok();
    }

    /// <summary>
    /// PUT: Updates game.
    /// </summary>
    /// <param name="game">Updated game.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPut]
    public async Task<ActionResult> UpdateGame(Game game, CancellationToken cancellationToken)
    {
        var command = new UpdateGameCommand
        {
            Game = game
        };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// DELETE: Deletes game.
    /// </summary>
    /// <param name="name">Name of the game to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpDelete("{name}")]
    public async Task<ActionResult> DeleteGame(string name, CancellationToken cancellationToken)
    {
        var command = new DeleteGameCommand
        {
            Name = name
        };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
