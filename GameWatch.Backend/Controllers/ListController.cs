using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;

/// <summary>
/// List controller.
/// </summary>
[ApiController]
[Route("api/lists")]
public class ListController : ControllerBase
{
    public IActionResult GetAllLists()
    {
        return View();
    }
}
