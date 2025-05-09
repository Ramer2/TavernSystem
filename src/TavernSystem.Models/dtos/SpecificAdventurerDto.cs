using TavernSystem.Models.models;

namespace TavernSystem.Models.dtos;

public class SpecificAdventurerDto
{
    public int id { get; set; }
    public string nickname { get; set; }
    public string race { get; set; }
    public string experienceLevel { get; set; }
    public Person personData { get; set; }

    public SpecificAdventurerDto()
    {
    }

    public SpecificAdventurerDto(int id, string nickname, string race, string experienceLevel, Person personData)
    {
        this.id = id;
        this.nickname = nickname;
        this.race = race;
        this.experienceLevel = experienceLevel;
        this.personData = personData;
    }
}