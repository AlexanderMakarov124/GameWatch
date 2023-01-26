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
    /// <returns>Status 201 - Created.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateGameAsync(GameDto gameDto, CancellationToken cancellationToken)
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

        return StatusCode(201);
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
