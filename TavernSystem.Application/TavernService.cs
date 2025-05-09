using System.Text.Json;
using System.Text.Json.Nodes;
using TavernSystem.Models.dtos;
using TavernSystem.Models.models;
using TavernSystem.Repositories;

namespace TavernSystem.Application;

public class TavernService : ITavernService
{
    private ITavernRepository _tavernRepository;

    public TavernService(ITavernRepository tavernRepository)
    {
        _tavernRepository = tavernRepository;
    }

    public List<AllAdventurersDto> GetAllAdventurers()
    {
        var adventurers = _tavernRepository.GetAllAdventurers();
        var result = new List<AllAdventurersDto>();

        foreach (var adventurer in adventurers)
        {
            result.Add(new AllAdventurersDto
            {
                Id = adventurer.Id,
                Nickname = adventurer.Nickname
            });
        }
        return result;
    }

    public SpecificAdventurerDto GetSpecificAdventurerDtoById(int adventurerId)
    {
        return _tavernRepository.GetSpecificAdventurerDtoById(adventurerId);
    }

    public bool RegisterAdventurer(JsonNode json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        Adventurer? adventurer;
        try
        {
            adventurer = JsonSerializer.Deserialize<Adventurer>(json, options);
        }
        catch
        {
            throw new ApplicationException("JSON deserialization failed. Seek help.");
        }

        if (adventurer == null)
        {
            throw new ApplicationException("JSON deserialization failed. Seek help.");
        }

        Console.WriteLine(adventurer);

        // bounty + person exists check
        if (_tavernRepository.GetBounty(adventurer.PersonId))
        {
            throw new ArgumentException("Cannot add adventurer with a bounty.");
        }

        if (_tavernRepository.AdventurerExists(adventurer.PersonId))
        {
            throw new ApplicationException("An adventurer is already registered for this person.");
        }

        _tavernRepository.AddAdventurer(adventurer);

        return true;
    }
}