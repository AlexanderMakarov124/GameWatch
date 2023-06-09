using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.UseCases.DTOs;
using GameWatch.UseCases.Games.CreateGame;
using GameWatch.UseCases.Games.DeleteGame;
using GameWatch.UseCases.Games.GetAllGames;
using GameWatch.UseCases.Games.GetGameById;
using GameWatch.UseCases.Games.GetGamesByName;
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
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// POST: Creates a game.
    /// </summary>
    /// <param name="gameDto">Game DTO.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <response code="201">Successfully created.</response>
    /// <returns>Status 201 - Created.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateGameAsync(GameDto gameDto, CancellationToken cancellationToken)
    {
        Game game; 

        try
        {
            game = await mediator.Send(new CreateGameCommand { GameDto = gameDto }, cancellationToken);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }

        return StatusCode(201, game);
    }

    /// <summary>
    /// GET all games.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Games.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllGames(CancellationToken cancellationToken)
    {
        var query = new GetAllGamesQuery();

        var games = await mediator.Send(query, cancellationToken);

        return Ok(games);
    }

    /// <summary>
    /// GET game by id.
    /// </summary>
    /// <param name="id">Game id.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Game.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameById(int id, CancellationToken cancellationToken)
    {
        var query = new GetGameByIdQuery
        {
            Id = id
        };

        Game game;

        try
        {
            game = await mediator.Send(query, cancellationToken);
        }
        catch (NotFoundException)
        {
            return BadRequest();
        }

        return Ok(game);
    }

    /// <summary>
    /// GET games that contain given name.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Games.</returns>
    [HttpGet("{name}")]
    public async Task<IActionResult> GetGamesByName(string name, CancellationToken cancellationToken)
    {
        var query = new GetGamesByNameQuery
        {
            Name = name
        };

        var games = await mediator.Send(query, cancellationToken);

        return Ok(games);
    }

    /// <summary>
    /// PUT: Updates game.
    /// </summary>
    /// <param name="game">Updated game.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateGame(Game game, CancellationToken cancellationToken)
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
    public async Task<IActionResult> DeleteGame(string name, CancellationToken cancellationToken)
    {
        var command = new DeleteGameCommand
        {
            Name = name
        };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
