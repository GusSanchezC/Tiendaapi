using MySql.Data.MySqlClient;
using System.Data;
using Tiendaapi.Modelos;

namespace Tiendaapi.Datos
{
    public class DatosProductos
    {
        DBConn conn = new DBConn();
        public async Task <List<ModeloProductos>> MostrarProductos()
        {
            var lista = new List<ModeloProductos>();
            using (var sql = new MySqlConnection(conn.CadenaSQL()))
            {
                using (var cmd = new MySqlCommand("mostrarProductos",sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var mproducto = new ModeloProductos();
                            mproducto.id = (int)reader["id"];
                            mproducto.descripcion = (string)reader["descripcion"];
                            mproducto.precio = (decimal)reader["precio"];
                            lista.Add(mproducto);
                        }
                    }
                } 
            }
            return lista;
        }
        public async Task InsertarProductos(ModeloProductos parametros)
        {
            using (var sql = new MySqlConnection(conn.CadenaSQL()))
            {
                using (var cmd = new MySqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_descripcion",parametros.descripcion);
                    cmd.Parameters.AddWithValue("_precio",parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EditarProductos(ModeloProductos parametros)
        {
            using (var sql = new MySqlConnection(conn.CadenaSQL()))
            {
                using (var cmd = new MySqlCommand("editarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_id", parametros.id);
                    cmd.Parameters.AddWithValue("_precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EliminarProductos(ModeloProductos parametros)
        {
            using (var sql = new MySqlConnection(conn.CadenaSQL()))
            {
                using (var cmd = new MySqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_id", parametros.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
