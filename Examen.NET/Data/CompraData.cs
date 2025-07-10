using Examen.NET.Entitys;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Data
{
    public class CompraData
    {
        private readonly SqlConn _conn;

        public CompraData(SqlConn conn)
        {
            _conn = conn;
        }

        public List<ClienteArticulo> getAllCompras()
        {
            var compras = new List<ClienteArticulo>();

            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand(@"
            SELECT ca.Id, ca.clienteId, ca.articuloId, ca.tiendaId, ca.cantidad, ca.fecha,
                   c.nombre, c.apellido,
                   a.descripcion, a.codigo, a.precio,
                   t.sucursal
            FROM clientesArticulo ca
            INNER JOIN clientes c ON ca.clienteId = c.Id
            INNER JOIN articulos a ON ca.articuloId = a.Id
            INNER JOIN tienda t ON ca.tiendaId = t.Id", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                compras.Add(new ClienteArticulo
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    ClienteId = reader.GetInt32(reader.GetOrdinal("clienteId")),
                    ArticuloId = reader.GetInt32(reader.GetOrdinal("articuloId")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                    Tienda = new Tienda
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("tiendaId")),
                        Sucursal = reader.GetString(reader.GetOrdinal("sucursal"))
                    },
                    Cliente = new Cliente
                    {
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("apellido"))
                    },
                    Articulo = new Articulo
                    {
                        Descripcion = reader.GetString(reader.GetOrdinal("descripcion")),
                        Codigo = reader.GetString(reader.GetOrdinal("codigo")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("precio"))
                    }
                });
            }

            conn.Close();
            return compras;
        }

        public List<ClienteArticulo> getComprasTienda(int tiendaId)
        {
            var compras = new List<ClienteArticulo>();

            using var conn = _conn.CreateConnection();
            conn.Open();

                    var cmd = new SqlCommand(@"
            SELECT ca.Id, ca.clienteId, ca.articuloId, ca.cantidad, ca.fecha,
                   c.nombre, c.apellido,
                   a.descripcion, a.codigo, a.precio,
                   t.sucursal, t.Id AS tiendaId
            FROM clientesArticulo ca
            INNER JOIN clientes c ON ca.clienteId = c.id
            INNER JOIN articulos a ON ca.articuloId = a.id
            INNER JOIN articulotienda at ON a.id = at.articuloId
            INNER JOIN tienda t ON at.tiendaId = t.id
            WHERE t.id = @tiendaId
            ", conn);

            cmd.Parameters.AddWithValue("@tiendaId", tiendaId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                compras.Add(new ClienteArticulo
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    ClienteId = reader.GetInt32(reader.GetOrdinal("clienteId")),
                    ArticuloId = reader.GetInt32(reader.GetOrdinal("articuloId")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                    Cliente = new Cliente
                    {
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("apellido"))
                    },
                    Articulo = new Articulo
                    {
                        Descripcion = reader.GetString(reader.GetOrdinal("descripcion")),
                        Codigo = reader.GetString(reader.GetOrdinal("codigo")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("precio"))
                    },
                    Tienda = new Tienda
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("tiendaId")),
                        Sucursal = reader.GetString(reader.GetOrdinal("sucursal"))
                    }
                });
            }

            conn.Close();
            return compras;
        }

        public void createCompra(ClienteArticulo compra)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            int tiendaId = 0;

            var cmdTienda = new SqlCommand("SELECT tiendaId FROM articuloTienda WHERE articuloId = @ArticuloId", conn);
            cmdTienda.Parameters.AddWithValue("@ArticuloId", compra.ArticuloId);

            var result = cmdTienda.ExecuteScalar();

            if (result == null)
            {
                tiendaId = 1;
                var cmdInsertTienda = new SqlCommand(@"
            INSERT INTO articuloTienda (articuloId, tiendaId)
            VALUES (@ArticuloId, @TiendaId)", conn);

                cmdInsertTienda.Parameters.AddWithValue("@ArticuloId", compra.ArticuloId);
                cmdInsertTienda.Parameters.AddWithValue("@TiendaId", tiendaId);

                cmdInsertTienda.ExecuteNonQuery();
            }
            else
            {
                tiendaId = Convert.ToInt32(result);
            }

            var cmd = new SqlCommand("INSERT INTO clientesArticulo (clienteId, articuloId, tiendaId, Cantidad, fecha) " +
                                     "VALUES (@ClienteId, @ArticuloId, @TiendaId, @Cantidad, @Fecha)", conn);

            cmd.Parameters.AddWithValue("@ClienteId", compra.ClienteId);
            cmd.Parameters.AddWithValue("@ArticuloId", compra.ArticuloId);
            cmd.Parameters.AddWithValue("@TiendaId", tiendaId);
            cmd.Parameters.AddWithValue("@Cantidad", compra.Cantidad);
            cmd.Parameters.AddWithValue("@Fecha", compra.Fecha);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<ClienteArticulo> getComprasCliente(int clienteId)
        {
            var compras = new List<ClienteArticulo>();

            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand(@"
            SELECT ca.Id, ca.clienteId, ca.articuloId, ca.tiendaId, ca.cantidad, ca.fecha,
                   c.nombre, c.apellido,
                   a.descripcion, a.codigo, a.precio,
                   t.sucursal
            FROM clientesArticulo ca
            INNER JOIN clientes c ON ca.clienteId = c.Id
            INNER JOIN articulos a ON ca.articuloId = a.Id
            INNER JOIN tienda t ON ca.tiendaId = t.Id
            WHERE ca.clienteId = @clienteId
        ", conn);

            cmd.Parameters.AddWithValue("@clienteId", clienteId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                compras.Add(new ClienteArticulo
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    ClienteId = reader.GetInt32(reader.GetOrdinal("clienteId")),
                    ArticuloId = reader.GetInt32(reader.GetOrdinal("articuloId")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                    Tienda = new Tienda
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("tiendaId")),
                        Sucursal = reader.GetString(reader.GetOrdinal("sucursal"))
                    },
                    Cliente = new Cliente
                    {
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("apellido"))
                    },
                    Articulo = new Articulo
                    {
                        Descripcion = reader.GetString(reader.GetOrdinal("descripcion")),
                        Codigo = reader.GetString(reader.GetOrdinal("codigo")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("precio"))
                    }
                });
            }

            conn.Close();
            return compras;
        }

        public void deleteCompra(int ClienteId, int ArticuloId)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM clientesArticulo WHERE ClienteId = @ClienteId AND ArticuloId = @ArticuloId", conn);

            cmd.Parameters.AddWithValue("@ClienteId", ClienteId);
            cmd.Parameters.AddWithValue("@ArticuloId", ArticuloId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool ClienteExiste(int clienteId)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT COUNT(1) FROM clientes WHERE Id = @ClienteId", conn);
            cmd.Parameters.AddWithValue("@ClienteId", clienteId);

            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }

        public bool ArticuloExiste(int articuloId)
        {
            using var conn = _conn.CreateConnection();
            conn.Open();

            var cmd = new SqlCommand("SELECT COUNT(1) FROM articulos WHERE Id = @ArticuloId", conn);
            cmd.Parameters.AddWithValue("@ArticuloId", articuloId);

            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }
    }
}
