namespace TavernSystem.Models.models;

public class Race
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Race()
    {
    }

    public Race(int id, string name)
    {
        Id = id;
        Name = name;
    }
}