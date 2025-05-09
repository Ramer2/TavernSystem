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
}