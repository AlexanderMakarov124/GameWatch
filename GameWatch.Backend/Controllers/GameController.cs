using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly ApplicationContext db;

    public GameController(ApplicationContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Produces("application/json")]
    public IEnumerable<Game> GetAllGames()
    {
        var games = db.Games;

        return games;
    }
}
