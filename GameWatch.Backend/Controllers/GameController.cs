using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;
public class GameController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
