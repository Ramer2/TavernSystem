namespace TavernSystem.Models.models;

public class Adventurer
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public int RaceId { get; set; }
    public int ExperienceId { get; set; }
    public int PersonId { get; set; }

    public Adventurer()
    {
    }

    public Adventurer(int id, string nickname, int raceId, int experienceId, int personId)
    {
        Id = id;
        Nickname = nickname;
        RaceId = raceId;
        ExperienceId = experienceId;
        PersonId = personId;
    }
}