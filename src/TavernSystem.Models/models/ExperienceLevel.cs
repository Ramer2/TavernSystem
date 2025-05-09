namespace TavernSystem.Models.models;

public class ExperienceLevel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ExperienceLevel()
    {
    }

    public ExperienceLevel(int id, string name)
    {
        Id = id;
        Name = name;
    }
}