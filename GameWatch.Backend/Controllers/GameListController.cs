using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameListController(ApplicationContext db, ILogger<GameController> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    /// <summary>
    /// GET all game lists.
    /// </summary>
    /// <returns>Game lists.</returns>
    public IEnumerable<GameList> GetAllGameLists()
    {
        var lists = db.GameLists;

        return lists;
    }
}
