using System.Data.SqlClient;
using APBD_Zad3.Models;

namespace APBD_Zad3.Repositories;

public class AnimalRepository(IConfiguration configuration) : IAnimalRepository
{
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        var orderByParameter = orderBy.ToLower() switch
        {
            "id" => "Id",
            "category" => "Category",
            "description" => "Description",
            "area" => "Area",
            _ => "Name"
        };

        command.CommandText = $"SELECT Id, Name, Description, Category, Area FROM Animals ORDER BY {orderByParameter}";

        connection.Open();
        using var reader = command.ExecuteReader();
        var animals = new List<Animal>();

        while (reader.Read())
        {
            animals.Add(new Animal
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                Category = (string)reader["Category"],
                Area = (string)reader["Area"]
            });
        }

        return animals;
    }

    public Animal GetAnimal(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Description, Category, Area FROM Animals WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();

        Animal animal = null!;

        if (reader.Read())
        {
            animal = new Animal
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                Category = (string)reader["Category"],
                Area = (string)reader["Area"]
            };
        }

        return animal;
    }

    public int CreateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        command.CommandText = "INSERT INTO Animals (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area); SELECT SCOPE_IDENTITY()";
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        connection.Open();
        var id = command.ExecuteScalar();
        return Convert.ToInt32(id);
    }

    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        command.CommandText = "UPDATE Animals SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", animal.Id);
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        connection.Open();
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Animals WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}