namespace TavernSystem.Repositories;

public class TavernRepository : ITavernRepository
{
    private string _connectionString;

    public TavernRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
}