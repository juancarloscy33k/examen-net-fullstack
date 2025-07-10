using Microsoft.Data.SqlClient;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Data
{
    public class ArticulosData
    {
        private readonly SqlConn _conn;

        public ArticulosData(SqlConn conn)
        {
            _conn = conn;
        }

        private Articulo MapArticulo(SqlDataReader reader)
        {
            return new Articulo
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Codigo = reader["Codigo"] as string,
                Descripcion = reader["Descripcion"] as string,
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                Imagen = reader["Imagen"] as string,
                Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
            };
        }

        public List<Articulo> allArticulos()
        {
            var articulos = new List<Articulo>();
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM articulos", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                articulos.Add(MapArticulo(reader));
            }
            conn.Close();
            return articulos;
        }

        public Articulo getArticuloId(int Id)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM articulos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return MapArticulo(reader);
            }
            conn.Close();
            return null;
        }

        public void createArticulo(Articulo articulo)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("INSERT INTO articulos (codigo, descripcion, precio, imagen, stock) VALUES (@Codigo, @Descripcion, @Precio, @Imagen, @Stock)", conn);

            cmd.Parameters.AddWithValue("@Codigo", articulo.Codigo);
            cmd.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
            cmd.Parameters.AddWithValue("@Precio", articulo.Precio);
            cmd.Parameters.AddWithValue("@Imagen", articulo.Imagen);
            cmd.Parameters.AddWithValue("@Stock", articulo.Stock);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void updateArticulo(Articulo articulo)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("UPDATE articulos SET codigo = @Codigo, descripcion = @Descripcion, precio = @Precio, imagen = @Imagen, stock = @Stock" +
                                        " WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Codigo", articulo.Codigo);
            cmd.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
            cmd.Parameters.AddWithValue("@Precio", articulo.Precio);
            cmd.Parameters.AddWithValue("@Imagen", articulo.Imagen);
            cmd.Parameters.AddWithValue("@Stock", articulo.Stock);
            cmd.Parameters.AddWithValue("@Id", articulo.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteArticulo(int Id)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM articulos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
