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
    [Route("/api/adventurers")]
    public IResult GetAllAdventurers()
    {
        var result = _tavernService.GetAllAdventurers();
        if (result.Count == 0) return Results.NotFound("No adventurers found in the database.");
        else return Results.Ok(result);
    }
    
    [HttpGet]
    [Route("/api/adventurers/{adventurerId}")]
    public IResult GetAdventurerById(int adventurerId)
    {
        var result = _tavernService.GetSpecificAdventurerDtoById(adventurerId);
        if (result == null) return Results.NotFound($"No adventurer with id '{adventurerId}' found in the database.");
        else return Results.Ok(result);
    }
    
    
}