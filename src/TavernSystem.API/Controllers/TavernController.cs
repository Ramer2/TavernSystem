using Microsoft.AspNetCore.Mvc;
using TavernSystem.Application;

namespace TavernSystem.API.Controllers;

[ApiController]
[Route("/api/adventurers/[controller]")]
public class TavernController : ControllerBase
{

    private ITavernService _tavernService;

    public TavernController(ITavernService tavernService)
    {
        _tavernService = tavernService;
    }

    [HttpGet]
    public IResult GetAllAdventurers()
    {
        return Results.NotFound();
    }
    
    [HttpGet]
    [Route("/api/adventurers/{adventurerId}")]
    public IResult GetAdventurerById(int adventurerId)
    {
        return Results.NotFound();
    }
    
    
}