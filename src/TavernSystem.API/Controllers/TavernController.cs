using System.Text.Json.Nodes;
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

    [HttpPost]
    [Route("/api/adventurers")]
    [Consumes("application/json")]
    public async Task<IResult> RegisterAdventurer()
    {
        using var reader = new StreamReader(Request.Body);
        var rawJson = await reader.ReadToEndAsync();

        var json = JsonNode.Parse(rawJson);
        if (json == null) return Results.BadRequest("Ivalid JSON format.");

        try
        {
            _tavernService.RegisterAdventurer(json);
        }
        catch (ArgumentException)
        {
            return Results.Forbid();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
        return Results.Created();
    }
}