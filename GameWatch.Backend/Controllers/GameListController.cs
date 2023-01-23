using AutoMapper;
using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using GameWatch.Infrastructure.Common;
using GameWatch.UseCases.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameWatch.Backend.Controllers;

/// <summary>
/// List of games controller.
/// </summary>
[ApiController]
[Route("api/gameLists")]
public class GameListController : ControllerBase
{
    private readonly ApplicationContext db;
    private readonly ILogger<GameController> logger;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameListController(ApplicationContext db, ILogger<GameController> logger, IMapper mapper)
    {
        this.db = db;
        this.logger = logger;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET all game lists.
    /// </summary>
    /// <returns>Game lists.</returns>
    [HttpGet]
    [Produces("application/json")]
    public IEnumerable<GameList> GetAllGameLists()
    {
        var lists = db.GameLists.Include(gl => gl.Games);

        logger.Log(LogLevel.Debug, "All game lists was successfully retrieved.");

        return lists;
    }

    /// <summary>
    /// GET game list by id.
    /// </summary>
    /// <param name="id">Given id.</param>
    /// <returns>Game list.</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    public GameList GetGameListById(int id)
    {
        var gameList = db.GameLists.First(gl => gl.Id == id);

        db.Entry(gameList).Collection(gl => gl.Games).Load();

        if (gameList == null)
        {
            var message = $"Game list with id {id} does not exist.";

            var exception = new NotFoundException(message);

            logger.Log(LogLevel.Error, message);

            throw exception;
        }

        return gameList;
    }

    /// <summary>
    /// POST: Creates game list.
    /// </summary>
    /// <param name="gameListDto">Game list DTO.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPost]
    public IActionResult CreateGameList(GameListDto gameListDto)
    {
        var gameList = mapper.Map<GameList>(gameListDto);

        db.GameLists.Add(gameList);
        db.SaveChanges();

        logger.LogDebug("Game list {Name} with id {Id} was successfully created.", gameList.Name, gameList.Id);

        return Ok();
    }

    /// <summary>
    /// PUT: Updates game list.
    /// </summary>
    /// <param name="gameList">Updated game list.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpPut]
    public ActionResult UpdateGameList(GameList gameList)
    {
        db.Update(gameList);
        db.SaveChanges();

        logger.LogDebug("Game list with id {Id} was successfully updated.", gameList.Id);

        return Ok();
    }

    /// <summary>
    /// DELETE: Deletes game list.
    /// </summary>
    /// <param name="id">Name of the game list to delete.</param>
    /// <returns>Status 200 - ok.</returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteGameList(int id)
    {
        var gameList = GetGameListById(id);

        if (gameList.Games.Any())
        {
            foreach (var game in gameList.Games)
            {
                db.Games.Remove(game);
            }
        }
        db.GameLists.Remove(gameList);
        db.SaveChanges();

        logger.LogDebug("Game list {Name} with id {Id} was successfully deleted.", gameList.Name, gameList.Id);

        return Ok();
    }
}
