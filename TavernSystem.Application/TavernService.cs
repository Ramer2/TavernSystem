using TavernSystem.Models.dtos;
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
        throw new NotImplementedException();
    }

    public SpecificAdventurerDto GetAdventurerById(int adventurerId)
    {
        throw new NotImplementedException();
    }
}