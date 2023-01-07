using GameWatch.DataAccess;
using GameWatch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly ApplicationContext db;

    public GameController(ApplicationContext db)
    {
        this.db = db;
    }

    public Game Get()
    {
        var game = db.Games.First();

        return game;
    }
}
