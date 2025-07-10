using Examen.NET.Entitys;
using Microsoft.Data.SqlClient;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Data
{
    public class ClienteData
    {
        private readonly SqlConn _conn;
        public ClienteData(SqlConn conn)
        {
            _conn = conn;
        }

        private Models.Cliente MapCliente(SqlDataReader reader)
        {
            return new Models.Cliente
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? null : reader.GetString(reader.GetOrdinal("Nombre")),
                Apellido = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? null : reader.GetString(reader.GetOrdinal("Apellido")),
                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                Passwor = reader.IsDBNull(reader.GetOrdinal("Passwor")) ? null : reader.GetString(reader.GetOrdinal("Passwor"))
            };
        }

        public List<Cliente> Clientes()
        {
            var Clientes = new List<Cliente>();

            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM clientes", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read()) {

                Clientes.Add(MapCliente(reader));

            }
            conn.Close();
            return Clientes;

        }

        public Cliente GetClientId(int Id)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM clientes WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return MapCliente(reader);
            }
            conn.Close();
            return null;
        }

        public void createClient(Cliente cliente)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("INSERT INTO clientes (nombre, apellido, direccion, correo, passwor)" +
                "VALUES(@Nombre, @Apellido, @Direccion, @Correo, @Password)", conn);

            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
            cmd.Parameters.AddWithValue("@Password", cliente.Passwor);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void updateClient(Cliente cliente)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("UPDATE clientes SET nombre = @Nombre, apellido = @Apellido, direccion = @Direccion, correo = @Correo, passwor = @Passwor " +
                "WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
            cmd.Parameters.AddWithValue("@Passwor", cliente.Passwor);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteClient(int Id)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM clientes WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public Cliente? Login(string correo, string passwor)
        {
            using var connection = _conn.CreateConnection();
            connection.Open();

            string query = "SELECT * FROM Clientes WHERE correo = @Correo AND passwor = @Passwor";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Correo", correo);
            command.Parameters.AddWithValue("@Passwor", passwor);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2),
                    Direccion = reader.GetString(3),
                    Correo = reader.GetString(4),
                    Passwor = reader.GetString(5)
                };
            }

            return null;
        }
    }
}
