using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace APBD5.AnimalsConf;
[Route("api/[controller]")]
[ApiController]
public class AnimalController : ControllerBase
{
    public MySqlConnection _connection;

    public AnimalController(MySqlConnection connection)
    {
        _connection = connection;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals(string orderBy = "name")
    {
        string query = $"SELECT * FROM Animals ORDER BY {orderBy}";
        var animals = new List<Animal>();
        _connection.Open();
        var cmd = new MySqlCommand(query, _connection);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var animal = new Animal(reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetString(reader.GetOrdinal("Description")),
                reader.GetString(reader.GetOrdinal("Category")),
                reader.GetString(reader.GetOrdinal("Area")));
            animals.Add(animal);
        }

        return Ok(animals);
        _connection.Close();

    }

    [HttpPost]
    public ActionResult<Animal> AddAnimal(Animal animal)
    {
        string query = @"INSERT INTO Animals (Name, Description, Category, Area) 
                             VALUES (@idanimal,@name, @description, @category, @area);
                             SELECT SCOPE_IDENTITY();";
        _connection.Open();
        using (var cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@idanimal", animal.Id);
            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@description", animal.Description);
            cmd.Parameters.AddWithValue("@category", animal.Category);
            cmd.Parameters.AddWithValue("@area", animal.Area);

            var newId = cmd.ExecuteScalar();
            animal.Id = Convert.ToInt32(newId);
        }

        return NoContent();
        _connection.Close();

    }

    [HttpPut("{idanimal}")]
    public IActionResult UpdateAnimal(int idanimal, Animal animal)
    {
        string query = @"UPDATE Animals 
                             SET Name = @Name, 
                                 Description = @Description, 
                                 Category = @Category, 
                                 Area = @Area 
                             WHERE idanimal = @idanimal;";

        _connection.Open();
        using (var cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@Name", animal.Name);
            cmd.Parameters.AddWithValue("@Description", animal.Description);
            cmd.Parameters.AddWithValue("@Category", animal.Category);
            cmd.Parameters.AddWithValue("@Area", animal.Area);
            cmd.Parameters.AddWithValue("@IdAnimal", idanimal);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                return NotFound();
            }
        }
        _connection.Close();
        return NoContent();
    }
    [HttpDelete("{idanimal}")]
    public IActionResult DeleteAnimal(int idanimal)
    {
        string query = @"DELETE FROM Animals WHERE idanimal = @IdAnimal;";

        _connection.Open();

        using (var cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@IdAnimal", idanimal);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                return NotFound();
            }
        }
        _connection.Close();
        return NoContent();
    }
    private  Animal GetAnimalById(int idanimal)
    {
        string query = "SELECT * FROM Animals WHERE idanimal = @IdAnimal;";
        Animal animal = null;

        using (var cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@IdAnimal", idanimal);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    animal = new Animal( reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                        reader.GetString(reader.GetOrdinal("Name")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetString(reader.GetOrdinal("Category")),
                        reader.GetString(reader.GetOrdinal("Area")));
                }
            }
        }
        _connection.Close();
        return animal;
    }

    
}