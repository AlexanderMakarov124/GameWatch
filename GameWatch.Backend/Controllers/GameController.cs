﻿using Microsoft.AspNetCore.Mvc;

namespace GameWatch.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    public IActionResult Get()
    {
        return View();
    }
}
