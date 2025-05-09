using System.Text.Json.Nodes;
using TavernSystem.Models.dtos;

namespace TavernSystem.Application;

public interface ITavernService
{
    public List<AllAdventurersDto> GetAllAdventurers();

    public SpecificAdventurerDto GetSpecificAdventurerDtoById(int adventurerId);
    
    public bool RegisterAdventurer(JsonNode json);
}