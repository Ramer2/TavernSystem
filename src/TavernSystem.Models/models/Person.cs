namespace TavernSystem.Models.models;

public class Person
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public bool HasBounty { get; set; }

    public Person()
    {
    }

    public Person(string id, string firstName, string middleName, string lastName, bool hasBounty)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        HasBounty = hasBounty;
    }
}