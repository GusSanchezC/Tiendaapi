using System;
using MySql.Data.MySqlClient;
public class DBConn
{
	private string _connectionString = string.Empty;
	public DBConn(string connectionString)
	{
        _connectionString = connectionString;
    }
    public DBConn()
    {
        var constructor = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        _connectionString = constructor.GetSection("ConnectionStrings:AmazonConnection").Value;
    }
    public string CadenaSQL()
    {
        return _connectionString;
    }
    public void TestConnection()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexión exitosa a MySQL.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
            }
        }
    }
    // Aquí puedes agregar métodos para realizar consultas y otras operaciones
    public void ExecuteQuery(string query)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
