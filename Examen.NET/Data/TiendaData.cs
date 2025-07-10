using Examen.NET.Entitys;
using Microsoft.Data.SqlClient;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Data
{
    public class TiendaData
    {
        private readonly SqlConn _conn;

        public TiendaData(SqlConn conn)
        {
            _conn = conn;
        }

        private Models.Tienda MapTienda(SqlDataReader reader)
        {
            return new Models.Tienda
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Sucursal = reader.IsDBNull(reader.GetOrdinal("Sucursal")) ? null : reader.GetString(reader.GetOrdinal("Sucursal")),
                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion"))
            };
        }
        
        public List<Tienda> allTiendas()
        {
            var tiendas = new List<Tienda>();
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM tienda", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tiendas.Add(MapTienda(reader));
            }
            conn.Close();
            return tiendas;

        }

        public Tienda getTiendaId (int Id)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM tienda WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return MapTienda(reader);
            }
            conn.Close();
            return null;
        }

        public void createTienda (Tienda tienda)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("INSERT INTO tienda (sucursal,direccion) " +
                "VALUES (@Sucursal,@Direccion)", conn);
            cmd.Parameters.AddWithValue("@Sucursal", tienda.Sucursal);
            cmd.Parameters.AddWithValue("@Direccion", tienda.Direccion);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void updateTienda(Tienda tienda)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("UPDATE tienda SET sucursal = @Sucursal, direccion = @Direccion WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Sucursal", tienda.Sucursal);
            cmd.Parameters.AddWithValue("@Direccion", tienda.Direccion);
            cmd.Parameters.AddWithValue("@Id", tienda.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteTienda (int Id)
        {
            var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM tienda WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue ("@Id", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
