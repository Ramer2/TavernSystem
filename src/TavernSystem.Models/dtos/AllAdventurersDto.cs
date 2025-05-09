namespace TavernSystem.Models.dtos;

public class AllAdventurersDto
{
    public int Id { get; set; }
    public string Nickname { get; set; }

    public AllAdventurersDto()
    {
    }

    public AllAdventurersDto(int id, string nickname)
    {
        Id = id;
        Nickname = nickname;
    }
}