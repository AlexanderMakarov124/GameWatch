using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common.Exceptions;
using GameWatch.UseCases.DTOs;
using GameWatch.UseCases.GameLists.CreateGameList;
using GameWatch.UseCases.GameLists.DeleteGameList;
using GameWatch.UseCases.GameLists.GetAllGameLists;
using GameWatch.UseCases.GameLists.GetGameListById;
using GameWatch.UseCases.GameLists.UpdateGameList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;

/// <summary>
/// List of games controller.
/// </summary>
[ApiController]
[Route("api/gameLists")]
public class GameListController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameListController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// GET all game lists.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Ok result with game lists.</returns>
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllGameLists(CancellationToken cancellationToken)
    {
        var gameLists = await mediator.Send(new GetAllGameListsQuery(), cancellationToken);

        return Ok(gameLists);
    }

    /// <summary>
    /// GET game list by id.
    /// </summary>
    /// <param name="id">Given id.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Ok result with game list.</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetGameListById(int id, CancellationToken cancellationToken)
    {
        var query = new GetGameListByIdQuery
        {
            Id = id
        };

        GameList gameList;

        try
        {
            gameList = await mediator.Send(query, cancellationToken);
        }
        catch (NotFoundException)
        {
            return BadRequest();
        }

        return Ok(gameList);
    }

    /// <summary>
    /// POST: Creates game list.
    /// </summary>
    /// <param name="gameListDto">Game list DTO.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateGameList(GameListDto gameListDto, CancellationToken cancellationToken)
    {
        var command = new CreateGameListCommand
        {
            GameListDto = gameListDto
        };

        try
        {
            await mediator.Send(command, cancellationToken);
        }
        catch (AlreadyExistException)
        {
            return BadRequest();
        }

        return StatusCode(201);
    }

    /// <summary>
    /// PUT: Updates game list.
    /// </summary>
    /// <param name="gameList">Updated game list.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateGameList(GameList gameList, CancellationToken cancellationToken)
    {
        var command = new UpdateGameListCommand
        {
            GameList = gameList
        };

        try
        {
            await mediator.Send(command, cancellationToken);
        }
        catch (AlreadyExistException)
        {
            return BadRequest();
        }

        return Ok();
    }

    /// <summary>
    /// DELETE: Deletes game list.
    /// </summary>
    /// <param name="id">Name of the game list to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameList(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteGameListCommand
        {
            Id = id
        };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
