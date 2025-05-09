using TavernSystem.Models.dtos;

namespace TavernSystem.Application;

public interface ITavernService
{
    public List<AllAdventurersDto> GetAllAdventurers();

    public SpecificAdventurerDto GetAdventurerById(int adventurerId);
}