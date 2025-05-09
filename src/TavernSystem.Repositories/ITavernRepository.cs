using TavernSystem.Models.dtos;
using TavernSystem.Models.models;

namespace TavernSystem.Repositories;

public interface ITavernRepository
{
    public List<Adventurer> GetAllAdventurers();

    public SpecificAdventurerDto GetSpecificAdventurerDtoById(int adventurerId);
    
    public bool GetBounty(string personId);
    
    public bool AdventurerExists(string personId);
    
    public bool AddAdventurer(Adventurer adventurer);
}