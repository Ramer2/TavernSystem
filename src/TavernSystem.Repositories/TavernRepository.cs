using System.Data;
using Microsoft.Data.SqlClient;
using TavernSystem.Models.dtos;
using TavernSystem.Models.models;

namespace TavernSystem.Repositories;

public class TavernRepository : ITavernRepository
{
    private string _connectionString;

    public TavernRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Adventurer> GetAllAdventurers()
    {
        const string query = "SELECT * FROM Adventurer";
        List<Adventurer> adventurers = new List<Adventurer>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        adventurers.Add(new Adventurer
                        {
                            Id = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            RaceId = reader.GetInt32(2),
                            ExperienceId = reader.GetInt32(3),
                            PersonId = reader.GetInt32(4)
                        });
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return adventurers;
    }

    public SpecificAdventurerDto GetSpecificAdventurerDtoById(int adventurerId)
    {
        const string query = @"SELECT * FROM Adventurer A 
                                    JOIN dbo.ExperienceLevel EL on A.ExperienceId = EL.Id
                                    JOIN dbo.Race R on R.Id = A.RaceId
                                    JOIN dbo.Person P on P.Id = A.PersonId
                                WHERE A.Id = @Id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", adventurerId);
            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return new SpecificAdventurerDto
                    {
                        id = reader.GetInt32(0),
                        nickname = reader.GetString(1),
                        race = reader.GetString(8),
                        experienceLevel = reader.GetString(6),
                        personData = new Person
                        {
                            Id = reader.GetString(9),
                            FirstName = reader.GetString(10),
                            MiddleName = reader.GetString(11),
                            LastName = reader.GetString(12),
                            HasBounty = reader.GetBoolean(13)
                        }
                    };
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return null;
    }
}