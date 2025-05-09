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
                            PersonId = reader.GetString(4)
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

    public bool GetBounty(string personId)
    {
        const string query = "SELECT P.HasBounty FROM Person P WHERE P.Id = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", personId);
            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        return reader.GetBoolean(0);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        throw new ApplicationException($"No person with id {personId} was found.");
    }

    public bool AdventurerExists(string personId)
    {
        const string query = "SELECT * FROM Adventurer A WHERE A.PersonId = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", personId);
            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                return reader.HasRows;
            }
            finally
            {
                reader.Close();
            }
        }
    }

    public bool AddAdventurer(Adventurer adventurer)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                var maxIdQuery = "SELECT MAX(Id) FROM Adventurer";
                var maxId = 0;
                SqlCommand idCommand = new SqlCommand(maxIdQuery, connection, transaction);
                SqlDataReader reader = idCommand.ExecuteReader();

                try
                {
                    if (reader.Read())
                    {
                        maxId = reader.GetInt32(0);
                    }
                }
                finally
                {
                    reader.Close();
                }

                // set the id
                adventurer.Id = maxId + 1;

                var insertAdventurerResult = -1;
                var insertAdventurerQuery =
                    "INSERT INTO Adventurer VALUES (@Id, @Nickname, @RaceId, @ExperienceId, @PersonId)";

                SqlCommand command = new SqlCommand(insertAdventurerQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", adventurer.Id);
                command.Parameters.AddWithValue("@Nickname", adventurer.Nickname);
                command.Parameters.AddWithValue("@RaceId", adventurer.RaceId);
                command.Parameters.AddWithValue("@ExperienceId", adventurer.ExperienceId);
                command.Parameters.AddWithValue("@PersonId", adventurer.PersonId);

                insertAdventurerResult = command.ExecuteNonQuery();
                if (insertAdventurerResult == -1)
                    throw new ApplicationException("Failed to insert adventurer.");

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        return true;
    }
}