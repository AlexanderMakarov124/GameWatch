using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.UseCases.Games.Commands.CreateGame;
using GameWatch.UseCases.Games.Commands.DeleteGame;
using GameWatch.UseCases.Games.Commands.UpdateGame;
using GameWatch.UseCases.Games.Queries.GetGameById;
using GameWatch.UseCases.Games.Queries.SearchGames;
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
    /// <response code="400">Creating failed.</response>
    /// <returns>Created game.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateGameAsync(CreateGameDto gameDto, CancellationToken cancellationToken)
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

        var gameUri = $"{Request.Path.Value}/{game.Id}";

        return Created(gameUri, game);
    }

    /// <summary>
    /// Search games.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <response code="200">Games was successfully fetched.</response>
    /// <returns>Games.</returns>
    [HttpGet]
    public async Task<IActionResult> SearchGames(CancellationToken cancellationToken)
    {
        var games = await mediator.Send(new SearchGamesQuery(), cancellationToken);

        return Ok(games);
    }

    /// <summary>
    /// GET game by id.
    /// </summary>
    /// <param name="id">Game id.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <response code="200">Game was successfully fetched.</response>
    /// <response code="404">Game was not found.</response>
    /// <returns>Game.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGameById(int id, CancellationToken cancellationToken)
    {
        Game game;

        try
        {
            game = await mediator.Send(new GetGameByIdQuery { Id = id }, cancellationToken);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return Ok(game);
    }

    /// <summary>
    /// PATCH: Updates game.
    /// </summary>
    /// <param name="id">Game id.</param>
    /// <param name="game">Updated game.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <response code="200">Games was successfully updated.</response>
    /// <returns>Status 200 - ok.</returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateGame(int id, UpdateGameDto game, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateGameCommand { Id = id, Game = game }, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// DELETE: Deletes game.
    /// </summary>
    /// <param name="name">Name of the game to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <response code="204">Game was successfully deleted and nothing to return.</response>
    /// <returns>Status 204 - no content.</returns>
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteGame(string name, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteGameCommand { Name = name }, cancellationToken);

        return NoContent();
    }
}
